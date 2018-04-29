using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowED : Event {
    [SerializeField]
    GameObject ED;

    public override void trigger()
    {
        Instantiate(ED);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        triggerNextEvent();
    }
}
