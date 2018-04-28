using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyMaxRelation : Event {

    public Event max, notMax;
    public bool started = false;

    public override void trigger()
    {
        nextEvent = max;
        triggerNextEvent();
    }

    private void Update()
    {
        Debug.Log("testUpdate");

        Debug.Log("LastCheck:" + FlagController.instance.speedyLastCheck + " current:" + FlagController.instance.speedyRelation);
        if (!FlagController.instance.triggerSpeedyDialog)
        {
            Debug.Log("LastCheck:" + FlagController.instance.speedyLastCheck + " current:" + FlagController.instance.speedyRelation);
            if (FlagController.instance.speedyLastCheck != FlagController.instance.speedyRelation)
            {
                FlagController.instance.speedyLastCheck = FlagController.instance.speedyRelation;
                if (FlagController.instance.speedyRelation >= 50 && !FlagController.instance.triggerSpeedyDialog)
                {
                    FlagController.instance.triggerSpeedyDialog = true;
                    StartCoroutine(tryStart());
                }
            }
        }

    }

    IEnumerator tryStart()
    {
        yield return new WaitUntil(() => GameMasterController.instance.startEvent());
        Debug.Log("Start");
        trigger();
    }
}

