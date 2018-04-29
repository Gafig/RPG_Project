using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicCare : Event {

    public override void trigger()
    {
        Debug.Log("Medic care");
        triggerNextEvent();
    }
}
