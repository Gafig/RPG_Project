using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Fade.instance.fadeIn();
        GameMasterController.instance.setPlayerToTheLastDoor();
    }

}
