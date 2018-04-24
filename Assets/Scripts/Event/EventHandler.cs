using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler:MonoBehaviour{

    [SerializeField]
    public StartEvent startEvent;

    public void triggerEvents()
    {
        startEvent.trigger();
    }

    private void Update()
    {
        Transform eventTranform = transform.FindChild("Event");
        if(eventTranform != null)
        {
            startEvent = eventTranform.GetChild(0).FindChild("StartEvent").GetComponent<StartEvent>();
        }
    }
}
