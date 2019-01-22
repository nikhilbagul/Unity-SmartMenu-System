using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData) 
    {
        //Debug.Log("On pointer enter");

        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && checkProperties(d.gameObject))
        {
            //change placeHolder parent from parentToReturnTo --> the dropZone parent list where placeholder will attach itself to
            d.parentToReturnTo = this.transform.parent;
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        //Debug.Log("On pointer exit");

        if (eventData.pointerDrag == null)
            return;
        
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.parentToReturnTo == this.transform.parent)
        {
            d.parentToReturnTo = d.startParent;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop complete!");

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (checkProperties(d.gameObject))
            setProperties(d.gameObject);
    }

    public bool checkProperties(GameObject itemToCheck)
    {
        int itemIDToCheck = itemToCheck.GetComponent<Draggable>().elementID;

        if (itemToCheck != null)
        {
            switch (this.gameObject.transform.parent.tag)
            {
                case "Playlist":
                    if (SceneManager.Songs[itemIDToCheck].elementType == ElementType.Song &&
                        SceneManager.Songs[itemIDToCheck].elementState != ElementState.isInPlaylist && 
                        SceneManager.Songs[itemIDToCheck].elementState != ElementState.inBothLists)
                    {
                        Debug.Log("NOT in both lists");
                        return true;
                    }
                    else
                        return false;

                case "Shortcuts":
                    if (SceneManager.Songs[itemIDToCheck].elementType == ElementType.Song &&
                        SceneManager.Songs[itemIDToCheck].elementState != ElementState.isInShortcuts &&
                        SceneManager.Songs[itemIDToCheck].elementState != ElementState.inBothLists)
                    {
                        Debug.Log("NOT in both lists");
                        return true;
                    }
                    else
                        return false;
                //case "Trash":
                //    if (SceneManager.Songs[itemIDToCheck].elementState != ElementState.isRemoved)
                //    {

                //    }
                //    break;
                //case "MusicList":
                //    break;
                //case "ContactList":
                //break;
                default:
                    return false;
            }
        }

        else
            return false;
    }

    public void setProperties(GameObject itemToSet)
    {
        int itemIDToSet = itemToSet.GetComponent<Draggable>().elementID;

        if (itemToSet != null)
        {
            switch (this.gameObject.transform.parent.tag)
            {
                case "Playlist":
                    if (SceneManager.Songs[itemIDToSet].elementType == ElementType.Song &&
                        SceneManager.Songs[itemIDToSet].elementState != ElementState.isInPlaylist)
                    {
                        if (SceneManager.Songs[itemIDToSet].elementState == ElementState.isInShortcuts)
                        {
                            SceneManager.Songs[itemIDToSet].elementState = ElementState.inBothLists;
                            Debug.Log("in both lists");
                        }
                        else
                            SceneManager.Songs[itemIDToSet].elementState = ElementState.isInPlaylist;
                        
                        itemToSet.GetComponent<Draggable>().parentToReturnTo = this.transform;
                        Debug.Log("dropped into playlist");
                    }
                    break;
                case "Shortcuts":
                    if (SceneManager.Songs[itemIDToSet].elementType == ElementType.Song &&
                        SceneManager.Songs[itemIDToSet].elementState != ElementState.isInShortcuts)
                    {
                        if (SceneManager.Songs[itemIDToSet].elementState == ElementState.isInPlaylist)
                        {
                            SceneManager.Songs[itemIDToSet].elementState = ElementState.inBothLists;
                            Debug.Log("in both lists");
                        }
                        else
                            SceneManager.Songs[itemIDToSet].elementState = ElementState.isInShortcuts;
                        
                        itemToSet.GetComponent<Draggable>().parentToReturnTo = this.transform;
                        Debug.Log("dropped into shortcuts");
                    }
                    break;
                case "Trash":
                    if (SceneManager.Songs[itemIDToSet].elementState != ElementState.isRemoved)
                    {
                        SceneManager.Songs[itemIDToSet].elementState = ElementState.isRemoved;
                    }
                    break;
                case "MusicList":
                    break;
                case "ContactList":
                    break;
                default:
                    break;
            }
        }
    }
}
