using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : Event {

    [SerializeField]
    string destination;

    [SerializeField]
    string doorId;

    public override void trigger()
    {
        GameMasterController.instance.startEvent();
        StartCoroutine(fadeOut());
        
    }

    private IEnumerator fadeOut()
    {
        Fade.instance.fadeOut();
        yield return new WaitUntil(() => !Fade.instance.isFading);
        StartCoroutine(load());
    }

    private IEnumerator load()
    {
        if (destination != null)
        {
            GameMasterController.instance.setLastDoorID(doorId);
            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(destination, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                print("Loading the Scene");
                yield return null;
                StartCoroutine(fadeIn());
            }
        }
    }

    private IEnumerator fadeIn()
    {
        GameMasterController.instance.endEvent();
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
    }
}
