using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEvent : Event {

    public InteractableObject obj;

    public override void trigger()
    {
        resetTrigger();
        GameMasterController.instance.endEvent();
    }

    private void resetTrigger()
    {
        if (obj != null)
            obj.hasInteracted = false;
    }
}
