using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : Event {

    public override void trigger()
    {
        Debug.Log("Sleep zzzz");
        triggerNextEvent();
    }
}
