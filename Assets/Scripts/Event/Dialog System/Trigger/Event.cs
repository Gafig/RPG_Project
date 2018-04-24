﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event:MonoBehaviour {

    [SerializeField]
    Event nextEvent;

    public void triggerNextEvent()
    {
        if(nextEvent != null)
            nextEvent.trigger();
    }

    public virtual void trigger() { }
}
