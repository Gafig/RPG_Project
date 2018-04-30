using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour {

	public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button select;
    public GameObject ui;
    public LevelControllerScript lc;

    public Event root;

    private void Start()
    {
        lc = GameObject.FindObjectOfType<LevelControllerScript>();
    }

    private void Update()
    {
        button1.interactable = FlagController.instance.dunTolevel[0];
        button2.interactable = FlagController.instance.dunTolevel[1];
        button3.interactable = FlagController.instance.dunTolevel[2];
        button4.interactable = FlagController.instance.dunTolevel[3];
        button5.interactable = FlagController.instance.dunTolevel[4];
    }

    public void cancel()
    {
        
        if (root != null)
        {
            Debug.Log("Trigger Next");
            root.triggerNextEvent();
        }
        toggle();
        select.interactable = true;
    }

    public void toggle()
    {
        select.interactable = false;
        ui.gameObject.SetActive(!ui.gameObject.activeSelf);
    }

    public void goTo(int level)
    {
        lc.currentFloor = level;
        GameMasterController.instance.setLastDoorID("DunEnt"+(level));
        lc.toNextLevel();
        cancel();
    }
}
