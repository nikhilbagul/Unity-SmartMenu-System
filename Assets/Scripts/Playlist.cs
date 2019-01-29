using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Playlist : DropZone 
{
    List<Draggable> playlistShortcuts;

    private void Start()
    {
        playlistShortcuts = new List<Draggable>();
    }

    public override void OnDropHandler(PointerEventData eventData)
    {
        if (eventData != null)
        {
            objectBeingDropped = eventData.pointerDrag.GetComponent<Draggable>();
            if (objectBeingDropped.original != null)
                objectToBeCompared = objectBeingDropped.original;
            else
                objectToBeCompared = objectBeingDropped;

            Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);

            if (objectBeingDropped.itemType == ItemType.Song)
                AddToPlaylist();
        }
    }

    public void AddToPlaylist()
    {
        GameObject objectToBeAddedToList = Instantiate(objectBeingDropped.gameObject, objectBeingDropped.placeHolderParent);
        objectToBeAddedToList.transform.SetSiblingIndex(objectBeingDropped.transform.GetSiblingIndex());
        if (objectBeingDropped.original == null)
            objectToBeAddedToList.GetComponent<Draggable>().original = objectBeingDropped;
        objectToBeAddedToList.GetComponent<Draggable>().parentToReturnTo = objectToBeAddedToList.transform.parent;
    }
}
