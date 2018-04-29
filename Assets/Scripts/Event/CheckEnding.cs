using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnding : Event {

    public override void trigger()
    {
        FlagController.instance.checkEnding();
        triggerNextEvent();
    }
}
