using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VadyFirstMeet : Event {

    public override void trigger()
    {
        StartCoroutine(tryStart());
    }

    IEnumerator tryStart()
    {
        yield return new WaitUntil(() => GameMasterController.instance.startEvent());
        triggerNextEvent();
    }

    private void Update()
    {
        if (FlagController.instance.restVisit == 0)
        {
            FlagController.instance.restVisit++;
            trigger();
        }
    }
}
