﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPopUpText : Event {

    [TextArea(3, 10)]
    public string str;
    [SerializeField]
    GameObject popUp;

    public GameObject ui;

    public override void trigger()
    {
            ui = Instantiate(popUp);
            ui.GetComponent<PopUpText>().ev = this;
            ui.GetComponent<PopUpText>().type(str);
    }
}
