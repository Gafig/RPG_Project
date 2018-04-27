using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewMidDay : Event {

    public override void trigger()
    {
        int date = DateTimeController.instance.getDate();
        DateTimeController.instance.setTimeTo(date + 1, 12, 0);
        triggerNextEvent();
    }

    private void Start()
    {
        DateTimeController.instance.getTime();
    }
}
