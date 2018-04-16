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
        yield return new WaitForSeconds(waitInSeconds);
        GameMasterController.instance.endEvent();
    }
}
