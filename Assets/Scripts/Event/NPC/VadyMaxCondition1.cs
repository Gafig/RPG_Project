using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VadyMaxCondition1 : Event {

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

        //Debug.Log("LastCheck:" + FlagController.instance.vadyLastCheck + " current:" + FlagController.instance.vadyRelation);
        if (!FlagController.instance.triggerVadyDialog)
        {
            //Debug.Log("LastCheck:" + FlagController.instance.vadyLastCheck + " current:" + FlagController.instance.vadyRelation);
            if (FlagController.instance.vadyLastCheck != FlagController.instance.vadyRelation)
            {
                FlagController.instance.vadyLastCheck = FlagController.instance.vadyRelation;
                if (FlagController.instance.vadyRelation >= 50 && !FlagController.instance.triggerVadyDialog)
                {
                    FlagController.instance.triggerVadyDialog = true;
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
