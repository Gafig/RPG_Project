using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : Event {

	public OpenDoor door;

    bool isTriggered = false;

    public override void trigger()
    {
        isTriggered = true;
        door.trigger();
    }

    private void Update()
    {
        if (isTriggered)
        {
            if (door.doneAnimate)
            {
                Debug.Log("Open Door");
                triggerNextEvent();
                isTriggered = false;
            }
        }
    }
}
