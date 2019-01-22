﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
    
    public Transform musicList;
    public Transform contactList;
    public static ListElement[] Songs;
    public static ListElement[] Contacts;
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
            Songs[i].id = musicList.GetChild(0).GetChild(i).GetComponent<Draggable>().elementID = i;
            Songs[i].elementType = ElementType.Song;
            Songs[i].elementState = ElementState.notInAnyList;
            musicList.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = "Song " + Songs[i].id;
        }

        for (int i = 0; i < contactList.GetChild(0).childCount; i++)
        {
            Contacts[i] = new ListElement();
            Contacts[i].id = contactList.GetChild(0).GetChild(i).GetComponent<Draggable>().elementID = i;
            Contacts[i].elementType = ElementType.Contact;
            Contacts[i].elementState = ElementState.notInAnyList;
            contactList.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = "Contact " + Contacts[i].id;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	    	
	}
}
