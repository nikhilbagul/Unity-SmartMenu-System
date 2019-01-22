using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    bool isParentChanged = false;
    Draggable draggableObject;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        //Debug.Log("On pointer enter");

        if (eventData.pointerDrag == null)
            return;

        draggableObject = eventData.pointerDrag.GetComponent<Draggable>();
        checkProperties(draggableObject);
        if (draggableObject != null && isParentChanged)
        {
            draggableObject.parentToReturnTo = this.transform.parent;
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        //Debug.Log("On pointer exit");

        if (eventData.pointerDrag == null)
            return;
        
        draggableObject = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggableObject != null && draggableObject.parentToReturnTo == this.transform.parent)
        {
            draggableObject.parentToReturnTo = draggableObject.startParent;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop complete!");

        draggableObject = eventData.pointerDrag.GetComponent<Draggable>();
        checkProperties(draggableObject);
        if (isParentChanged)
            setProperties(draggableObject);
    }

    public void checkProperties(Draggable itemToCheck)
    {
        int itemIDToCheck = itemToCheck.elementID;
        ElementType typeToCheck = itemToCheck.elementType;

        if (itemToCheck != null)
        {
            if (this.gameObject.transform.tag == "Playlist" && typeToCheck == ElementType.Song)
            {
                if (SceneManager.Songs[itemIDToCheck].elementType == ElementType.Song &&
                    SceneManager.Songs[itemIDToCheck].elementState != ElementState.isInPlaylist &&
                    SceneManager.Songs[itemIDToCheck].elementState != ElementState.inBothLists)
                {
                    isParentChanged = true;
                }
            }

            if (this.gameObject.transform.tag == "Shortcuts" && typeToCheck == ElementType.Song)
            {
                if (SceneManager.Songs[itemIDToCheck].elementType == ElementType.Song &&
                    SceneManager.Songs[itemIDToCheck].elementState != ElementState.isInShortcuts &&
                    SceneManager.Songs[itemIDToCheck].elementState != ElementState.inBothLists)
                {
                    isParentChanged = true;
                }
            }

            if (this.gameObject.transform.tag == "Shortcuts" && typeToCheck == ElementType.Contact)
            {
                if (SceneManager.Contacts[itemIDToCheck].elementType == ElementType.Contact &&
                    SceneManager.Contacts[itemIDToCheck].elementState != ElementState.isInShortcuts)
                {
                    isParentChanged = true;
                }
            }

            if (this.gameObject.transform.tag == "Trash")
            {
                isParentChanged = true;
            }
         }
    }

    public void setProperties(Draggable itemToSet)
    {
        int itemIDToSet = itemToSet.elementID;
        ElementType typeToCheck = itemToSet.elementType;

        if (itemToSet != null)
        {
            // Check for playlist and Songs
            if (this.gameObject.transform.tag == "Playlist" && typeToCheck == ElementType.Song)
            {
                if (SceneManager.Songs[itemIDToSet].elementType == ElementType.Song &&
                    SceneManager.Songs[itemIDToSet].elementState != ElementState.isInPlaylist)
                {
                    if (SceneManager.Songs[itemIDToSet].elementState == ElementState.isInShortcuts)
                    {
                        SceneManager.Songs[itemIDToSet].elementState = ElementState.inBothLists;
                        Debug.Log("Song in both lists");
                    }
                    else
                        SceneManager.Songs[itemIDToSet].elementState = ElementState.isInPlaylist;

                    itemToSet.parentToReturnTo = this.transform;
                    Debug.Log("Song dropped into playlist");
                }
            }

            // Check for shortcuts and Songs
            if (this.gameObject.transform.tag == "Shortcuts" && typeToCheck == ElementType.Song)
            {
                if (SceneManager.Songs[itemIDToSet].elementType == ElementType.Song &&
                    SceneManager.Songs[itemIDToSet].elementState != ElementState.isInShortcuts)
                {
                    if (SceneManager.Songs[itemIDToSet].elementState == ElementState.isInPlaylist)
                    {
                        SceneManager.Songs[itemIDToSet].elementState = ElementState.inBothLists;
                        //Debug.Log("Song in both lists");
                    }
                    else
                        SceneManager.Songs[itemIDToSet].elementState = ElementState.isInShortcuts;

                    itemToSet.parentToReturnTo = this.transform;
                    Debug.Log("Song dropped into shortcuts");
                }
            }

            // Check for shortcuts and Contacts
            if (this.gameObject.transform.tag == "Shortcuts" && typeToCheck == ElementType.Contact)
            {
                if (SceneManager.Contacts[itemIDToSet].elementType == ElementType.Contact &&
                    SceneManager.Contacts[itemIDToSet].elementState != ElementState.isInShortcuts)
                {
                    SceneManager.Contacts[itemIDToSet].elementState = ElementState.isInShortcuts;

                    itemToSet.parentToReturnTo = this.transform;
                    Debug.Log("Contact dropped into shortcuts");
                }
            }

            //Check for move to Trash
            if (this.gameObject.transform.tag == "Trash")
            {
                itemToSet.parentToReturnTo = this.transform;

                if (typeToCheck == ElementType.Contact)
                {
                    //-------------------------------------------------------Delete contacts------------------------------------------------------------------------------------------
                    if (SceneManager.Contacts[itemIDToSet].elementState == ElementState.isInShortcuts)
                    {
                        // Remove contact shortcut
                        if (itemToSet.startParent.tag == "Shortcuts")
                        {
                            itemToSet.DestroyReplacementUIElement();
                            SceneManager.Contacts[itemIDToSet].elementState = ElementState.notInAnyList;
                            Destroy(itemToSet.gameObject);    
                        }

                        // Remove contact from the system
                        if (itemToSet.startParent.tag == "ContactList")
                        {
                            Draggable[] list = GameObject.FindWithTag("Shortcuts").transform.GetComponentsInChildren<Draggable>();
                            foreach (Draggable item in list)
                            {
                                if (item.elementID == itemIDToSet && item.elementType == ElementType.Contact)
                                {
                                    Destroy(item.gameObject);
                                }
                            }

                            itemToSet.DestroyReplacementUIElement();
                            SceneManager.Contacts[itemIDToSet].elementState = ElementState.isRemoved;
                            Destroy(itemToSet.gameObject);
                        }
                    }

                    // Remove contact from the system
                    else if (SceneManager.Contacts[itemIDToSet].elementState == ElementState.notInAnyList)
                    {
                        itemToSet.DestroyReplacementUIElement();
                        SceneManager.Contacts[itemIDToSet].elementState = ElementState.isRemoved;
                        Destroy(itemToSet.gameObject);
                    }
                }
                //-------------------------------------------------------Delete songs------------------------------------------------------------------------------------------
                if (typeToCheck == ElementType.Song)
                {
                        // Remove Song shortcut
                        if (itemToSet.startParent.tag == "Shortcuts" || itemToSet.startParent.tag == "Playlist")
                        {
                            itemToSet.DestroyReplacementUIElement();
                            SceneManager.Songs[itemIDToSet].elementState = ElementState.notInAnyList;
                            Destroy(itemToSet.gameObject);
                        }

                        // Remove Song from the system
                        if (itemToSet.startParent.tag == "MusicList")
                        {
                            Draggable[] list = GameObject.FindWithTag("Shortcuts").transform.GetComponentsInChildren<Draggable>();
                            foreach (Draggable item in list)
                            {
                                if (item.elementID == itemIDToSet && item.elementType == ElementType.Song)
                                {
                                    Destroy(item.gameObject);
                                }
                            }

                            list = GameObject.FindWithTag("Playlist").transform.GetComponentsInChildren<Draggable>();
                            foreach (Draggable item in list)
                            {
                                if (item.elementID == itemIDToSet && item.elementType == ElementType.Song)
                                {
                                    Destroy(item.gameObject);
                                }
                            }

                            itemToSet.DestroyReplacementUIElement();
                            SceneManager.Songs[itemIDToSet].elementState = ElementState.isRemoved;
                            Destroy(itemToSet.gameObject);
                        }
                }
            }
        }

        isParentChanged = false;
    }
}
