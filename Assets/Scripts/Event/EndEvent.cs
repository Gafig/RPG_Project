using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEvent : Event {

    public override void trigger()
    {
        GameMasterController.instance.endEvent();
    }
}
