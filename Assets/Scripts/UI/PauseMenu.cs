using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField]
    GameObject panel;
    bool isOn;
	// Use this for initialization
	void Start () {
        panel.SetActive(false);
        isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isOn)
            {
                toggleGame();
            }
        }

    }

    public void toggleGame()
    {
        isOn = !isOn;
        Time.timeScale = (Time.timeScale == 1) ? 0 : 1 ;
        panel.SetActive(!panel.activeSelf);
        //Disable scripts that still work while timescale is set to 0
    }
}
