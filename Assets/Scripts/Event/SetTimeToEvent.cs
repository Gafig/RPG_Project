using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeToEvent : Event {

    [Range(0, 24)]
    public int hour;
    [Range(0, 60)]
    public int minute;

    public override void trigger()
    {
        DateTimeController.instance.setTimeTo(DateTimeController.instance.getDate(), hour, minute);
        triggerNextEvent();
    }
}
