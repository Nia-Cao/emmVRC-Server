# emmVRC-Server

c# API implementation for emmVRC

This is a project to replicate the emmVRC API server in C# using the .net WEB API. Based on work from ERROR#0418 and reverse-engineering the emmVRC mod itself.

Basic core functions are now implemented. First code release is a bit messy and has debug info in the terminal.

PIN's not encrypted and they are created by entering in the emmVRC mod as usual.

Requires MongoDB installed locally with a database named "emmVRC".

Currently no avatar favourites as I completely neglected to be bothered with it. I mostly wrote this to learn a few things and to get access to my avatar favs that I fav'd using ERROR's server. No reply to checks for risky functions either.

You're on your own with regards to getting emmVRC to connect to it for the time being. If anyone wishes to write a mod to patch the ApiURL then I can link to it providing full source is provided and any binaries are un-obfuscated and will decompile back into fully readable source using DNSpy or DotPeek. Nothing malicious here.

I recommend building this from source even if any binaries end up in the repo as there may be debug crap that get's left in before I push. When it's feature complete I shall release it properly

## Currently working:
    [x]Basic authentication
    
    [x]Avatar list loading
    
    [x]Avatar info loading upon selection
    
    [x]Pedestal scanning
    
    [x]Avatar Searching
    
## To-do
   
    Messaging
    
    Avatar favourites
    
## Planned features
    No PIN creation in game, instead this will be handled by a Discord bot
        ^Currently in game as I don't plan on running this at any scale unless demand arises.
    
    Companion mod to automatically patch emmVRC at startup so no more need to edit the dll every update
        ^In progress, currently re-writing the network configuration to use localhost as the API server
    
    No need for VRC+ to favourite avatars. All that crappy (required) decision is doing is pushing people to less savoury mods
    
    emmVRC mod itself still checks for VRC+ and I will not be patching that out, server doesn't care though.
    
    emmVRC import feature using bot, just enter your user id and PIN and your favs are transferred over and pin created.
        -May result in a ban from emmVRC.
        -emmVRC being behind Cloudflare has put a stop to that one, easier to write a patch to dump avatar data as JSON
        Unless API was changed to prevent that? Easy enough to grab missing data from VRC API in any case
        
    User and avatar checks to prevent creation of random accounts and insertion of bad avatar data
    

Future plans may include RemodCE support too using the same DB. REDIS support could also be added although for a personal server that seems a bit overkill.


Jen xoxo
