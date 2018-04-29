using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : Event {

    public override void trigger()
    {
        StartCoroutine(doFadeOut());
    }

    IEnumerator doFadeOut()
    {
        Fade.instance.fadeOut();
        yield return new WaitUntil(() => !Fade.instance.isFading);
        triggerNextEvent();
    }
}
