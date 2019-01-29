using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trash : DropZone 
{
    public Transform shorcutsList;
    public Transform playlist;

    bool isShortcut = false;
    List<Draggable> trashListItems;

    private void Start()
    {
        trashListItems = new List<Draggable>();
    }

    public override void OnDropHandler(PointerEventData eventData)
    {
        if (eventData != null)
        {
            objectBeingDropped = eventData.pointerDrag.GetComponent<Draggable>();
            if (objectBeingDropped.original != null)
            {
                //Destroy the shortcut
                objectToBeCompared = objectBeingDropped.original;
                isShortcut = true;
            }
            else
            {
                //Destroy the original
                objectToBeCompared = objectBeingDropped;
                isShortcut = false;
            }

            Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);
            AddToTrash();
        }
    }

    public void AddToTrash()
    {
        if (!isShortcut)
        {
            GameObject objectToBeAddedToList = Instantiate(objectBeingDropped.gameObject, objectBeingDropped.placeHolderParent);
            objectToBeAddedToList.transform.SetSiblingIndex(objectBeingDropped.transform.GetSiblingIndex());
            objectToBeAddedToList.GetComponent<Draggable>().parentToReturnTo = objectToBeAddedToList.transform.parent;
            objectToBeAddedToList.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
            objectToBeAddedToList.GetComponent<Image>().raycastTarget = false;
            trashListItems.Add(objectToBeAddedToList.GetComponent<Draggable>());

            foreach (Transform child in objectBeingDropped.parentToReturnTo)
            {
                if (child.GetComponent<Draggable>() == null)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (Transform child in shorcutsList)
            {
                if (child.GetComponent<Draggable>().original == objectBeingDropped)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (Transform child in playlist)
            {
                if (child.GetComponent<Draggable>().original == objectBeingDropped)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        else
        {
            foreach (Transform child in objectToBeCompared.placeHolderParent)
            {
                if (child.GetComponent<Draggable>() == null)
                {
                    Destroy(child.gameObject);
                }
            }        
        }

        Destroy(objectBeingDropped.gameObject);
    }
}
