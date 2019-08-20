# Unity-SmartMenu-System

The project contains a menu system with the following features: 

1. Media
2. Contacts
3. Shortcuts
4. Playlists
5. Trash

## Behavior set

- There are two types of lists: 
  1. Main lists (Media, Contacts)
  2. Quick Access lists (Shortcuts, Playlist, Trash)
  
- Only Songs can be dropped into **Playlist**. Moving songs to the **Playlist** creates a shortcut to the song in the Playlist

- Songs and Contacts can be dropped from anywhere into **Shortcuts**, thus creating shortcuts to the original Item

- When shortcuts are created, the original item remains in the list it was dragged from

- Dragging an item from **Media** or **Contacts** to the **Trash** will remove the item and all shortcuts to it

- There are no duplicate items in any of the lists, except the **Playlist** (we might want to listen to the same song twice)

- Dragging an item from a **Quick access list** into the **Trash** will remove the Shortcut in that list, but not the original item in the **Media** or **Contacts** list

### Media 

> - This list contains UI elements that are containers to media items like Soundtracks
> - This list will never have any duplicate items
> - All the elements in this list are draggable and snap back if not dropped onto a target

### Contacts 

> - This list contains UI elements that are containers to items like Contact cards
> - This list will never have any duplicate items
> - All the elements in this list are draggable and snap back if not dropped onto a target

### Shorcuts 

> - This list contains UI elements that are shortcuts to media and contact list items
> - This list will never have any duplicate items
> - All the elements in this list are draggable and snap back if not dropped onto a target

### Playlist 

> - This list contains UI elements that are shorcuts to media items like soundtracks from the Media list
> - This list can have duplicate items
> - All the elements in this list are draggable and snap back if not dropped onto a target

### Trash 

> - This list contains UI elements that are containers to media items like soundtracks and contact cards
> - This list will never have any duplicate items
> - All the elements in this list are draggable and snap back if not dropped onto a target



The project leverages the following Unity features:

1. Unity UI System
2. Unity Event System
