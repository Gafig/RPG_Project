using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangeonLevelConnector : MonoBehaviour {

    LevelControllerScript levelController;
    void OnTriggerEnter(Collider other)
    {
        levelController = GameObject.Find("LevelController").GetComponent(typeof(LevelControllerScript)) as LevelControllerScript;
        levelController.toNextLevel();
    }
}
