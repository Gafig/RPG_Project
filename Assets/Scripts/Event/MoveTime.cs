using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTime : Event {

    [SerializeField]
    int timeToAdd;

    public override void trigger()
    {
        DateTimeController.instance.updateTime(timeToAdd);
        triggerNextEvent();
    }
}
