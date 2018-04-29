using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOut : Event {

    public override void trigger()
    {
        Debug.Log("Eat");
        triggerNextEvent();
    }
}
