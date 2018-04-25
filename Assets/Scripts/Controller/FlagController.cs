using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagController : MonoBehaviour {

    public static FlagController instance;

    public bool SwordFlag;
    public bool REGULARFlag;

    public bool triggeredEnding = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
    }

    public void checkEnding()
    {
        if (DateTimeController.instance.getDate() >= 6)
        {
            Debug.Log("Check");
            if (!triggeredEnding)
            {
                triggeredEnding = true;
                startEnding();
            }
        }
    }

    private void startEnding()
    {
        string ending;
        if (SwordFlag) {
            if (REGULARFlag)
                ending = "Ending04";
            else
                ending = "Ending02";
        }
        else
        {
            if (REGULARFlag)
                ending = "Ending03";
            else
                ending = "Ending01";
        }
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(ending, LoadSceneMode.Single);
    }
}
