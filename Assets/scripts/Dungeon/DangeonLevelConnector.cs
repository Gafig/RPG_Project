using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangeonLevelConnector : MonoBehaviour {

    public string id;
    public Direction dir;
    private static bool isDone;

    private void Start()
    {
        isDone = false;
    }

    LevelControllerScript levelController;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && GameMasterController.instance.IsInputEnabled)
        {
            if (!isDone)
            {
                Debug.Log("Da hell?");
                levelController = GameObject.Find("LevelController").GetComponent<LevelControllerScript>();
                GameMasterController.instance.setLastDoorID(id);
                if (dir == Direction.down)
                    levelController.toLowerLevel();
                else
                    levelController.toHigherLevel();
                levelController.toNextLevel();
            }
        }
    }
}
