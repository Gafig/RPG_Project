using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : Event {

    string destination = "MainMenu";

    bool hasInteracted = false;

    public override void trigger()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            StartCoroutine(fadeOut());
        }
    }

    private IEnumerator fadeOut()
    {
        Fade.instance.fadeOut();
        yield return new WaitUntil(() => !Fade.instance.isFading);
        load();
    }

    void load()
    {
        if (destination != null)
        {
            //GameMasterController.instance.endEvent();
            Destroy(GameObject.Find("GameController"));
            Destroy(GameObject.Find("PermanantUI"));
            Destroy(GameObject.Find("TimeController"));
            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(destination, LoadSceneMode.Single);
        }
    }
}
