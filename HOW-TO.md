# How-to

First install MongoDB. I chose this as I have previously used ERROR's custom server and had hundreds of avatars stored in there, so it made sense to make this compatible.

Create a Mongo database called "emmVRC". Collections should be automagically created.

To get this working you need to get the emmVRC mod to point towards "http://localhost". It can then load the configuration and connect to the API server. How you do this is entirely up to you.

emmVRC should try to connect and request you enter a PIN. Upon entry it will store the PIN, generate bearer and login tokens and log you in. Future logins will use this same PIN.

By visiting worlds with public avatar pedestals it should begin building up a database of avatars that you can search through and select.

When avatars favouriting is added (most likely before anyone has even looked at this) you will still require VRC+ to use it. I won't release a patch that disables those checks.

That's about it for now. As with any mods for things, you use this at your own risk. Although it is a standalone application, you do need to break the emmVRC EULA to use it. It was stated on the emmVRC GitHub issue regarding making the mod open source that custom server projects are of no concern to the team, however that view may change at any time.

