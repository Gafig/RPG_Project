using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    void Start()
    {
        Fade.instance.fadeIn();
        GameMasterController.instance.setPermanantUI(true);
        GameMasterController.instance.setShowTime(true);
        GameMasterController.instance.setPlayerToTheLastDoor();
    }

}
