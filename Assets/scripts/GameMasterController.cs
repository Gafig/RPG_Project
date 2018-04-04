using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterController : MonoBehaviour {

    public static bool IsInputEnabled = true;
    public static bool betweenDialog = false;
    // Use this for initialization

	private GameObject levelControler;
    void Start () {
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
