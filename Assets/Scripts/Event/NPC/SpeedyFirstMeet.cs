using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyFirstMeet : Event {

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
        if (FlagController.instance.hqVisit == 0)
        {
            FlagController.instance.hqVisit++;
            trigger();
        }
    }
}
