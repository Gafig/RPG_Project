using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUI : Event {

    public override void trigger()
    {
        GameMasterController.instance.setPermanantUI(true);
        triggerNextEvent();
    }
}
