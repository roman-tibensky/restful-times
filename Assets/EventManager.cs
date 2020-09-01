﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    private Dictionary <string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if(!eventManager) {
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;
                if(!eventManager) {
                    Debug.Log("what the frack");
                } else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init () {
        if( eventDictionary ==null) 
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening (string eventName, UnityAction listener) {

    }

}
