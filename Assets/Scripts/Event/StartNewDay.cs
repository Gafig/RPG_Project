using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewDay : Event {

    public override void trigger()
    {
        int date = DateTimeController.instance.getDate();
        DateTimeController.instance.setTimeTo(date + 1, 8, 0 );
        triggerNextEvent();
    }

    private void Start()
    {
        DateTimeController.instance.getTime();
    }
}
