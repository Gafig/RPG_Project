using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventByTime : Event {

    public int startAt;
    public int triggered = 0;

    public override void trigger()
    {
        if (DateTimeController.instance.getcurrentTimeInDayInMinute() > startAt)
        {
            Debug.Log("Trigger by time");
            if (triggered < DateTimeController.instance.getDate())
            {
                Debug.Log("triggered");
                triggered++;
                GameMasterController.instance.startEvent();
                triggerNextEvent();
            }
        }
    }

    private void Update()
    {
    }
}
