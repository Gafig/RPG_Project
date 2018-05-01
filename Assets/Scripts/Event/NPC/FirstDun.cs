using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDun : Event {

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
        if (FlagController.instance.dunVisit == 0)
        {
            FlagController.instance.dunVisit++;
            trigger();
        }
    }
}
