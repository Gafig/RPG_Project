using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteScene : Event {
    [SerializeField]
    private string sceneName;

    public override void trigger()
    {
        if (sceneName != null)
            triggerNextEvent();
            SceneManager.UnloadSceneAsync(sceneName);
    }
}
