using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : Event {

    public bool isTrigger = false;
    public bool isStarted = false;
    public override void trigger()
    {
        StartCoroutine(tryStart());
    }

    IEnumerator tryStart()
    {
        yield return new WaitUntil(() => GameMasterController.instance.startEvent());
        isStarted = true;
        triggerNextEvent();
    }

    private void Update ()
    {
        if (Party.instance != null)
        {
            if (!isTrigger)
            {
                if (!Party.instance.head.isAlive())
                {
                    isTrigger = true;
                    trigger();
                }
            }
            else
            {
                if (isStarted)
                {
                    if (!GameMasterController.instance.betweenEvent)
                    {
                        isTrigger = false;
                        isStarted = false;
                    }
                }
            }
        }
    }
}
