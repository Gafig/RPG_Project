using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicCare : Event {

    public override void trigger()
    {
        Debug.Log("Medic care");
        if (Party.instance != null)
            foreach (Character c in Party.instance.partyList)
                c.fullHeal();
        triggerNextEvent();
    }
}
