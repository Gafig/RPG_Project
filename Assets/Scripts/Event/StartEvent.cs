using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : Event {

    public override void trigger()
    {
        GameMasterController.instance.startEvent();
        triggerNextEvent();
    }
}
