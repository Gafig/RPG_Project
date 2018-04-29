using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToDoor : Event {
    public string doorID;
    public override void trigger()
    {
        GameMasterController.instance.setLastDoorID(doorID);
        GameMasterController.instance.setPlayerToTheLastDoor();
        triggerNextEvent();
    }
}
