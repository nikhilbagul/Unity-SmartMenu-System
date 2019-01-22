using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public GameObject UIElementPrefab;
    public int elementID;

    //[HideInInspector]
    public Transform parentToReturnTo;
    //[HideInInspector]
    public Transform startParent;
    //[HideInInspector]
    public Vector3 startPosition;

    private GameObject replacementUIElement;
    private int startSiblingIndex;

    void Awake()
    {

    }


    public void OnBeginDrag(PointerEventData eventData) 
    {
        Debug.Log("Drag begin");

        startParent = this.transform.parent;
        parentToReturnTo = startParent;
        startPosition = this.transform.position;
        startSiblingIndex = this.transform.GetSiblingIndex();

        //Remove the object being dragged from the current list to prevent it from being Masked by the list
        this.transform.SetParent(parentToReturnTo.parent);
        this.GetComponent<Image>().raycastTarget = false;

        replacementUIElement = Instantiate(this.gameObject);
        replacementUIElement.transform.SetParent(startParent);
        replacementUIElement.transform.position = startPosition;
        replacementUIElement.transform.SetSiblingIndex(startSiblingIndex);
    }

    public void OnDrag(PointerEventData eventData) 
    {
        //Debug.Log("Dragging");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        Debug.Log("Drag end");

        if (parentToReturnTo != startParent)
        {
            //Drop to another list
            this.transform.SetParent(parentToReturnTo);
        }
        else
        {
            this.transform.SetParent(startParent);
            this.transform.position = startPosition;
            this.transform.SetSiblingIndex(startSiblingIndex);
            Debug.Log(this.transform.GetSiblingIndex());

            Debug.Log("Returning back to start position");

            Destroy(replacementUIElement);
        }

        this.GetComponent<Image>().raycastTarget = true;
    }
}
