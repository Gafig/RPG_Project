using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddLoad : Event
{

    [SerializeField]
    string destination;

    [SerializeField]
    string doorId;

    public override void trigger()
    {
        StartCoroutine(load());

    }

    private IEnumerator load()
    {
        if (destination != null)
        {
            GameMasterController.instance.setLastDoorID(doorId);
            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(destination, LoadSceneMode.Additive);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
                StartCoroutine(fadeIn());
            }
        }
    }

    private IEnumerator fadeIn()
    {
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
        triggerNextEvent();
    }
}
