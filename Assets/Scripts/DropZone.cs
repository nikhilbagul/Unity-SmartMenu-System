using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData) {

        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            //change placeHolder parent from parentToReturnTo --> the dropZone parent list where placeholder will attach itself to
            d.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {

        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeHolderParent == transform)
        {
            d.placeHolderParent = d.parentToReturnTo;
            //Destroy();
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        //Check properties

        checkProperties(d.gameObject);
        if (d != null) {
            d.parentToReturnTo = transform;
        }
        //Set properties
    }

    public void checkProperties(GameObject itemToCheck)
    {
        if (itemToCheck != null)
        {
            switch (this.gameObject.transform.parent.tag)
            {
                case "Playlist":
                    if (itemToCheck.GetComponent<ElementPropertyManager>().elementProperties.elementType == ElementType.Song && 
                        itemToCheck.GetComponent<ElementPropertyManager>().elementProperties.elementState != ElementState.isInPlaylist)
                    {
                        itemToCheck.GetComponent<ElementPropertyManager>().elementProperties.elementState = ElementState.isInPlaylist;
                    }
                    break;
                case "Shortcuts":
                    break;
                case "Trash":
                    if (itemToCheck.GetComponent<ElementPropertyManager>().elementProperties.elementState != ElementState.isRemoved)
                    {
                        itemToCheck.GetComponent<ElementPropertyManager>().elementProperties.elementState = ElementState.isRemoved;
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

    public void setProperties()
    {
        
    }
}
