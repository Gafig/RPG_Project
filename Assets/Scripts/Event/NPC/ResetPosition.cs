using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : Event {

    public int direction = 180;

    public override void trigger()
    {
        transform.root.GetComponent<ControllMovement>().setDirection(direction);
        triggerNextEvent();
    }
}
