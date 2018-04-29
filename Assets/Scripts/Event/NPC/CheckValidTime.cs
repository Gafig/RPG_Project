using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckValidTime : Event
{

    [SerializeField]
    Event valid;
    [SerializeField]
    Event invalid;
    public int timeLimitInMinutes;

    public override void trigger()
    {
        if (DateTimeController.instance.getcurrentTimeInDayInMinute() > timeLimitInMinutes)
            nextEvent = invalid;
        else
            nextEvent = valid;
        triggerNextEvent();
    }
}
