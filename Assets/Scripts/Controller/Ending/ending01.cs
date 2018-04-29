using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending01 : MonoBehaviour {

	void Start () {
        Destroy(GameObject.Find("Player"));
        GameMasterController.instance.setShowTime(false);
    }
}
