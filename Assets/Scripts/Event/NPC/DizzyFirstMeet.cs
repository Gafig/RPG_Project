using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyFirstMeet : Event {

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
        if (FlagController.instance.hospitalVisit == 0)
        {
            FlagController.instance.hospitalVisit++;
            trigger();
        }
    }
}
