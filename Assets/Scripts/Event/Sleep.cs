using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : Event {

    public override void trigger()
    {
        Debug.Log("Sleep zzzz");
        if (Party.instance != null)
            foreach (Character c in Party.instance.partyList)
                c.heal((int)(c.maxHP * 0.2f));
        triggerNextEvent();
    }
}
