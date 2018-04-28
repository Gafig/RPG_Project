using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyCondition : Event {

    public int dizzyEvent = 1;

    [SerializeField]
    Event onDate;
    [SerializeField]
    Event outDate;


    public override void trigger()
    {
        if (DateTimeController.instance.getDate() == dizzyEvent)
            nextEvent = onDate;
        else
            nextEvent = outDate;
        triggerNextEvent();
    }


}
