using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeneLevelSelector : Event {
    [SerializeField]
    GameObject ui;
    LevelSelecter ls;

    private void Start()
    {
        ui = GameObject.Find("LevelSelector");
        ls = ui.GetComponent<LevelSelecter>();
    }

    public override void trigger()
    {
        ls.root = this;
        ls.toggle();
    }
}
