using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCemera : Event {

    [SerializeField]
    Camera targetCamera;
    Camera[] cameras;

    public override void trigger()
    {
        cameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cameras)
            c.enabled = false;
        targetCamera.enabled = true;
        triggerNextEvent();
    }
}
