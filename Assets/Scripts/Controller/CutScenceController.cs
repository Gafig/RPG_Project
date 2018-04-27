using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenceController : MonoBehaviour {

    public bool fade;

	// Use this for initialization
	void Start () {
        if (fade)
            Fade.instance.fadeIn();
	}
}
