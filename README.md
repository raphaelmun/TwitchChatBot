TwitchChatBot
=============

IRC Chat Bot for TwitchTV

This is an IRC Chat Bot to add to your Twitch channel to respond to custom commands.
I originally created this back when I was playing League of Legends and streaming on Twitch. This is a generalized version for others to use.

Configuration:

	Edit the twitch.config file with your bot's Twitch.tv account username
    and the channel you want it to join. To generate the password,
    visit http://www.twitchapps.com/tmi/ and connect using your bot's account.
    The password should look something like "oauth:123987598712".

Adding Commands:

	To add a custom command, edit the <commands> section in the twitch.config file
    with your command. For example, if you want to add a "!test" command,
    add the line <test>This is a test!</test>.
    
Build & Run:

	Open in Visual Studio (requires .NET) and build. Then run.

License
=======

The bindings are licensed under the MIT X11 license:

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Authors
=======
Raphael Mun