using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : Event {
    public override void trigger()
    {
        transform.root.GetComponent<ControllMovement>().facePlayer();
        triggerNextEvent();
    }
}
