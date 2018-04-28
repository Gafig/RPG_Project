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

    bool hasInteracted = false;

    bool hasLoaded = false;
    string sceneName;

    public override void trigger()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            Scene scene = SceneManager.GetActiveScene();
            sceneName = scene.name;
            StartCoroutine(fadeOut());
        }
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
                yield return null;
                triggerNextEvent();
            }
        }
    }
}
