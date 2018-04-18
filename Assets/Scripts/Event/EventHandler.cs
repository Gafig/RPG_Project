using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler:MonoBehaviour{

    [SerializeField]
    public Event[] events;

    public void triggerEvents()
    {
        GameMasterController.instance.startEvents(events);
    }

}
