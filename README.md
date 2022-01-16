# emmVRC-Server
c# API implementation for emmVRC

This is a project to replicate the emmVRC API server in C# using the .net WEB API. Based on work from ERROR#0418 and reverse-engineering the emmVRC mod itself.

# Currently working:
    Basic authentication, just spits out a token with no PIN verification
    
    Avatar list but doesn't use the new /info endpoint yet
    
# Planned features
    No PIN creation in game, instead this will be handled by a Discord bot
    
    Companion mod to automatically patch emmVRC at startup so no more need to edit the dll every update
    
    No need for VRC+ to favourite avatars. All that crappy (required) decision is doing is pushing people to less savoury mods
    
    emmVRC import feature using bot, just enter your user id and PIN and your favs are transferred over and pin created.
        -May result in a ban from emmVRC.
        
    User and avatar checks to prevent creation of random accounts and insertion of bad avatar data
    
More to come plus code when the basic core features are implemented.
