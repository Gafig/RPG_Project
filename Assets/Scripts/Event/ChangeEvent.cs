using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEvent : Event {

    public GameObject replacment; 

    public override void trigger()
    {
        if (replacment != null)
        {
            GameObject g = Instantiate(replacment);
            g.transform.parent = transform.parent.parent;
            GameMasterController.instance.endEvent();
            Destroy(transform.parent.gameObject);
        }
        triggerNextEvent();
    }
}
