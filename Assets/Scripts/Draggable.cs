using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


    public Transform listElementPrefab;

    [HideInInspector]
    public Transform parentToReturnTo;
    [HideInInspector]
    public Transform placeHolderParent;
    [HideInInspector]
    public GameObject replacingListItem;

    void Awake()
    { 
    }

    public void OnBeginDrag(PointerEventData eventData) {

        if (this.GetComponent<ElementPropertyManager>().elementProperties.elementState == ElementState.notInAnyList)
        {
            replacingListItem = Instantiate(this.gameObject, this.transform.parent);
            replacingListItem.AddComponent<ElementPropertyManager>();
            replacingListItem.GetComponent<ElementPropertyManager>().elementProperties = this.GetComponent<ElementPropertyManager>().elementProperties;

            //replacingListItem.transform.SetParent(transform.parent);
            //LayoutElement le = replacingListItem.AddComponent<LayoutElement>();
            //le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            //le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
            //le.flexibleWidth = 0;
            //le.flexibleHeight = 0;

            replacingListItem.transform.SetSiblingIndex(transform.GetSiblingIndex());

            parentToReturnTo = transform.parent;
            placeHolderParent = parentToReturnTo;
            transform.SetParent(transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;    
        }
    }

    public void OnDrag(PointerEventData eventData) 
    {
        transform.position = eventData.position;

        //if (replacingListItem.transform.parent != placeHolderParent)
        //{
        //    replacingListItem.transform.SetParent(placeHolderParent);
        //}
        
        int newSiblingIndex = placeHolderParent.childCount;

		//reorder items in the same list
        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (transform.position.y > placeHolderParent.GetChild(i).position.y)
            {
                newSiblingIndex = i;

                //if (replacingListItem.transform.GetSiblingIndex() < newSiblingIndex)
                    //newSiblingIndex--;                
                break;
            }
        }

        //replacingListItem.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentToReturnTo);
        transform.SetSiblingIndex(replacingListItem.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (this.GetComponent<ElementPropertyManager>().elementProperties.elementState == ElementState.isRemoved)
            Destroy(replacingListItem);
    }
}
