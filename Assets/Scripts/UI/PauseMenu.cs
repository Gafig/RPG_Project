using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField]
    GameObject panel;
	// Use this for initialization
	void Start () {
        panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleGame();
        }

    }

    private void toggleGame()
    {
        Time.timeScale = (Time.timeScale == 1) ? 0 : 1 ;
        panel.SetActive(!panel.activeSelf);
        //Disable scripts that still work while timescale is set to 0
    }
}
