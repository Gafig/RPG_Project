﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameMasterController.instance.setShowTime(false);
        GameMasterController.instance.setPermanantUI(false);
	}
}