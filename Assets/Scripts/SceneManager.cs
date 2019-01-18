using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
    
    public Transform musicList;
    public Transform contactList;
    [SerializeField]
    private ListElement[] Songs;
    [SerializeField]
    private ListElement[] Contacts;
    bool isInitialized = false;


    private void Awake()
    {
        if (musicList.GetChild(0).childCount != 0)
            Songs = new ListElement[musicList.GetChild(0).childCount];
        if (contactList.GetChild(0).childCount != 0)
            Contacts = new ListElement[contactList.GetChild(0).childCount];
        
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < musicList.GetChild(0).childCount; i++)
        {
            Songs[i] = new ListElement();
            Songs[i].id = i;
            Songs[i].elementType = ElementType.Song;
            musicList.GetChild(0).GetChild(i).GetComponent<ElementPropertyManager>().elementProperties = Songs[i];
            musicList.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = "Song " + Songs[i].id;
        }

        for (int i = 0; i < contactList.GetChild(0).childCount; i++)
        {
            Contacts[i] = new ListElement();
            Contacts[i].id = i;
            Contacts[i].elementType = ElementType.Contact;
            contactList.GetChild(0).GetChild(i).GetComponent<ElementPropertyManager>().elementProperties = Contacts[i];
            contactList.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = "Contact " + Contacts[i].id;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	    	
	}
}
