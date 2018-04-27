using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagController : MonoBehaviour {

    public static FlagController instance;

    public bool SwordFlag;
    public bool REGULARFlag;
    public int hospitalVisit, hqVisit, dunVisit, restVisit;

    public bool triggeredEnding = false;

    public int dizzyRelation = 0;
    public int speedyRelation = 0;
    public int vadyRelation = 0;
    public bool[] dunTolevel = { false, false, false, false, false };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (dizzyRelation >= 100 && speedyRelation >= 100 && vadyRelation >= 100)
            REGULARFlag = true;
    }

    public void checkEnding()
    {
        if (DateTimeController.instance.getDate() >= 7)
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

    public void updateRelation(string character, int amount)
    {
        if (character.Equals("Dizzy"))
        {
            dizzyRelation += amount;
        }
    }
}
