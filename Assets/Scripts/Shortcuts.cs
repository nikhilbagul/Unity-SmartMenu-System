using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shortcuts : DropZone
{
    bool addToShorcuts = false;
    bool objectFound = false;
    List<Draggable> shortcuts;

    private void Start()
    {
        shortcuts = new List<Draggable>();
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

            if (this.transform.childCount != 0)
            {
                foreach (Draggable obj in this.transform.GetComponentsInChildren<Draggable>())
                {
                    if (obj.original == objectToBeCompared)
                    {
                        Debug.Log("Object already in shorcuts");
                        addToShorcuts = false;
                        objectFound = true;
                        break;
                    }                   
                }

                if(!objectFound)
                {
                    Debug.Log("Object not in shorcuts");
                    Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);
                    addToShorcuts = true;
                }

                objectFound = false;
            }
            else
            {
                Debug.Log("First object in dropped in list");
                Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);
                addToShorcuts = true;
            }

            if (addToShorcuts)
                AddToShortcuts();
        }
    }

    public void AddToShortcuts()
    {
        GameObject objectToBeAddedToList = Instantiate(objectBeingDropped.gameObject, objectBeingDropped.placeHolderParent);
        objectToBeAddedToList.transform.SetSiblingIndex(objectBeingDropped.transform.GetSiblingIndex());
        if (objectBeingDropped.original == null)
            objectToBeAddedToList.GetComponent<Draggable>().original = objectBeingDropped;
        objectToBeAddedToList.GetComponent<Draggable>().parentToReturnTo = objectToBeAddedToList.transform.parent;
        addToShorcuts = false;
    }
}
