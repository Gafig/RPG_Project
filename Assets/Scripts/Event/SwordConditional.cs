using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordConditional : Event {

    public Event trueEvent, falseEvent;
    
    public override void trigger()
    {
        if (FlagController.instance.SwordFlag)
            nextEvent = trueEvent;
        else
            nextEvent = falseEvent;
        triggerNextEvent();
    }
}
