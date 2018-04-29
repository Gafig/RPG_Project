using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyMaxRelation : Event {

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

        Debug.Log("LastCheck:" + FlagController.instance.dizzyLastCheck + " current:" + FlagController.instance.dizzyRelation);
        if (!FlagController.instance.triggerDizzyDialog)
        {
            Debug.Log("LastCheck:" + FlagController.instance.dizzyLastCheck + " current:" + FlagController.instance.dizzyRelation);
            if (FlagController.instance.dizzyLastCheck != FlagController.instance.dizzyRelation)
            {
                FlagController.instance.dizzyLastCheck = FlagController.instance.dizzyRelation;
                if (FlagController.instance.dizzyRelation >= 50 && !FlagController.instance.triggerDizzyDialog)
                {
                    FlagController.instance.triggerDizzyDialog = true;
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
