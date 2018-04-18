using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Event {
    [SerializeField]
    private float waitInSeconds;

    public override void trigger()
    {
        GameMasterController.instance.startEvent();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(waitInSeconds);
        print(Time.time);
        GameMasterController.instance.endEvent();
    }
}
