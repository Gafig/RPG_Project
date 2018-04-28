using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : Event {

    public override void trigger()
    {
        StartCoroutine(tryStart());
    }

    IEnumerator tryStart()
    {
        yield return new WaitUntil(() => GameMasterController.instance.startEvent());
        triggerNextEvent();
    }
}
