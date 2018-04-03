using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : Event {

    [SerializeField]
    string destination;

    public void trigger()
    {
        GameMasterController.instance.startEvent();
        if (destination != null)
        {
            Debug.Log("Load!");
            SceneManager.LoadScene(destination, LoadSceneMode.Single);
        }
        GameMasterController.instance.endEvent();
    }
}
