using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static partial class UIBehaviourExtensions
{
#if !(UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5)


    public static void AddListener(this UIBehaviour uiBehaviour, EventTriggerType eventID, UnityAction<BaseEventData> callback)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = eventID;
        entry.callback.AddListener(callback);

        var eventTriggers = (uiBehaviour.GetComponent<EventTrigger>() ?? uiBehaviour.gameObject.AddComponent<EventTrigger>()).triggers;
        eventTriggers.Add(entry);
    }

    public static void RemoveAllListeners(this UIBehaviour uiBehaviour, EventTriggerType eventID)
    {
        var eventTrigger = uiBehaviour.GetComponent<EventTrigger>();

        if (eventTrigger == null)
            return;

        eventTrigger.triggers.RemoveAll(listener => listener.eventID == eventID);
    }

    //public static void AddListener(this uiBehaviour, EventTriggerType eventID, UnityAction<BaseEventData> callback)
    //{
    //    var entry = new EventTrigger.Entry();
    //    entry.eventID = eventID;
    //    entry.callback.AddListener(callback);

    //    var eventTriggers = (uiBehaviour.GetComponent<EventTrigger>() ?? uiBehaviour.gameObject.AddComponent<EventTrigger>()).triggers;
    //    eventTriggers.Add(entry);
    //}

    //public static void RemoveAllListeners(this UIBehaviour uiBehaviour, EventTriggerType eventID)
    //{
    //    var eventTrigger = uiBehaviour.GetComponent<EventTrigger>();

    //    if (eventTrigger == null)
    //        return;

    //    eventTrigger.triggers.RemoveAll(listener => listener.eventID == eventID);
    //}


#endif
}