using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultCamera : MonoBehaviour {

    [SerializeField]
    Camera defautCamera;
    public Camera[] cameras;

    // Use this for initialization
    void Start () {
        Debug.Log("set Cam");
        cameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cameras)
            c.enabled = false;
        defautCamera.enabled = true;
	}
}
