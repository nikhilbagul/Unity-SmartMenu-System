using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ItemType
{
    Song = 1,
    Contact = 2
}

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    [HideInInspector]
    public Transform parentToReturnTo;
    [HideInInspector]
    public Transform placeHolderParent;
    [HideInInspector]
    public Draggable original;
    [HideInInspector]
    public ItemType itemType;

    private GameObject placeHolder;
    private int parentSiblingIndex;

    public void OnBeginDrag(PointerEventData eventData) 
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentToReturnTo = transform.parent;
        placeHolderParent = parentToReturnTo;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData) 
    {
        transform.position = eventData.position;

        if (placeHolderParent == parentToReturnTo)
        {
            int newSiblingIndex = placeHolderParent.childCount;

            //reorder items in the same list
            for (int i = 0; i < placeHolderParent.childCount; i++)
            {
                if (transform.position.y > placeHolderParent.GetChild(i).position.y)
                {
                    newSiblingIndex = i;

                    if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;
                    break;
                }
            }

            placeHolder.transform.SetSiblingIndex(newSiblingIndex);
            parentSiblingIndex = newSiblingIndex;
        }

        //Debug.Log("dragging");
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        //Return the current draggable back to parent list
        transform.SetParent(parentToReturnTo);
        transform.SetSiblingIndex(parentSiblingIndex);

        if (placeHolderParent == parentToReturnTo)
            Debug.Log("drag complete in parent list");
        else
            Debug.Log("drag complete in shortcut list");

        Destroy(placeHolder);
    }

    //for song sharing
    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (itemType == ItemType.Contact)
        {
            if (draggable.itemType == ItemType.Song)
            {
                Debug.Log(draggable.GetComponentInChildren<Text>().text + " --> SHARED with --> " + this.gameObject.GetComponentInChildren<Text>().text);
            }
        }       
    }
}
