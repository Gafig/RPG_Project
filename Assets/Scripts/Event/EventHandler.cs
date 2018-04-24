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

}
