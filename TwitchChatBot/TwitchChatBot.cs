using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Sharkbite.Irc;
using System.Timers;
using System.Linq;
using System.Xml.Linq;

namespace TwitchChatBot
{
    public class TwitchChatBot
    {
        private Connection connection;
        Dictionary<string, string> commands = new Dictionary<string,string>();

        public TwitchChatBot()
        {
            XDocument doc = XDocument.Load("twitch.config");
            XElement configElement = doc.Element("configuration");
            XElement serverElement = configElement.Element("Server");
            Credentials.Server = serverElement.Value;
            XElement channelElement = configElement.Element("Channel");
            Credentials.Channel = channelElement.Value;
            XElement nickElement = configElement.Element("Nick");
            Credentials.Nick = nickElement.Value;
            XElement passwordElement = configElement.Element("Password");
            Credentials.Password = passwordElement.Value;
            XElement commandsElement = configElement.Element("Commands");
            foreach(XElement commandElement in commandsElement.Elements())
            {
                commands.Add("!" + commandElement.Name.LocalName.ToLower(), commandElement.Value);
            }

            ConnectionArgs cargs = new ConnectionArgs(Credentials.Nick, Credentials.Server);
            cargs.Port = 6667;
            cargs.ServerPassword = Credentials.Password;

            connection = new Connection(cargs, false, false);

            connection.Listener.OnRegistered += new RegisteredEventHandler(OnRegistered);
            connection.Listener.OnJoin += new JoinEventHandler(OnJoin);
            connection.Listener.OnPublic += new PublicMessageEventHandler(OnPublic);
            //Listen for bot commands sent as private messages
            connection.Listener.OnPrivate += new PrivateMessageEventHandler(OnPrivate);
            //Listen for notification that an error has ocurred
            connection.Listener.OnError += new ErrorMessageEventHandler(OnError);

            //Listen for notification that we are no longer connected.
            connection.Listener.OnDisconnected += new DisconnectedEventHandler(OnDisconnected);
        }

        public void start()
        {
            try
            {
                connection.Connect();
                Console.WriteLine(Credentials.Nick + " connected.");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error during connection process.");
                Console.WriteLine(e);
            }
        }

        public void OnRegistered()
        {
            try
            {
                connection.Sender.Join(Credentials.Channel);
                Console.WriteLine("Joined " + Credentials.Channel);
                connection.Sender.PublicMessage(Credentials.Channel, "Hi Everyone!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in OnRegistered(): " + e);
            }
        }

        public void OnJoin(UserInfo user, string channel)
        {
            if(user.Nick == Credentials.Nick)
            {
            }
            else
            {
                Console.WriteLine(user.Nick + " connected.");
                connection.Sender.PublicMessage(Credentials.Channel, "Welcome " + user.Nick + "!");
            }
        }

        public void OnPublic(UserInfo user, string channel, string message)
        {
            if(message.Contains("!"))
            {
                string[] messageCommands = message.Split(' ');
                string command = messageCommands[0].ToLower();

                if(commands.ContainsKey(command))
                {
                    connection.Sender.PublicMessage(Credentials.Channel, commands[command]);
                }
            }
        }

        public void OnPrivate(UserInfo user, string message)
        {
            //Quit IRC if someone sends us a 'die' message
            if(message == "die")
            {
                connection.Disconnect("Bye");
            }
        }

        public void OnError(ReplyCode code, string message)
        {
            //All anticipated errors have a numeric code. The custom Thresher ones start at 1000 and
            //can be found in the ErrorCodes class. All the others are determined by the IRC spec
            //and can be found in RFC2812Codes.
            Console.WriteLine("An error of type " + code + " due to " + message + " has occurred.");
        }

        public void OnDisconnected()
        {
            //If this disconnection was involutary then you should have received an error
            //message ( from OnError() ) before this was called.
            Console.WriteLine("Connection to the server has been closed.");
        }
    }
}
