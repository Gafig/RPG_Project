using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : Event {

    public override void trigger()
    {
        StartCoroutine(doFadeIn());
    }

    IEnumerator doFadeIn()
    {
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
        triggerNextEvent();
    }
}
