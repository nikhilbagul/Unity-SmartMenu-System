using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
    [HideInInspector]
    public Draggable objectBeingDropped;
    [HideInInspector]
    public Draggable objectToBeCompared;

    public void OnPointerEnter(PointerEventData eventData) 
    {

        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeHolderParent == transform)
        {
            d.placeHolderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData) 
    {
        OnDropHandler(eventData);
    }

    public virtual void OnDropHandler(PointerEventData eventData)
    {
        //override in specific lists
    }
}
