using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    void Start()
    {
        GameMasterController.instance.setPlayerToTheLastDoor();
    }

}
