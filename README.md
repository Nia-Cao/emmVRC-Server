# emmVRC-Server

c# API implementation for emmVRC

This is a project to replicate the emmVRC API server in C# using the .net WEB API. Based on work from ERROR#0418 and reverse-engineering the emmVRC mod itself.

## Currently working:
    [x]Basic authentication
    
    [x]Avatar list loading
    
    [x]Avatar info loading upon selection
    
## To-do
    Avatar search
    
    Messaging
    
    Pedestal scanning
    
## Planned features
    No PIN creation in game, instead this will be handled by a Discord bot
    
    Companion mod to automatically patch emmVRC at startup so no more need to edit the dll every update
        ^In progress, currently re-writing the network configuration to use localhost as the API server
    
    No need for VRC+ to favourite avatars. All that crappy (required) decision is doing is pushing people to less savoury mods
    
    emmVRC import feature using bot, just enter your user id and PIN and your favs are transferred over and pin created.
        -May result in a ban from emmVRC.
        -emmVRC being behind Cloudflare has put a stop to that one, easier to write a patch to dump avatar data as JSON
        Unless API was changed to prevent that? Easy enough to grab missing data from VRC API in any case
        
    User and avatar checks to prevent creation of random accounts and insertion of bad avatar data
    
More to come plus code when the basic core features are implemented.
