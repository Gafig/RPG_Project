using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterController : MonoBehaviour {

    public bool IsInputEnabled = true;
    public bool betweenEvent = false;
    public Event currentEvent;

    public static GameMasterController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool startEvent()
    {
        if (betweenEvent)
            return false;
        betweenEvent = true;
        IsInputEnabled = false;
        return true;
    }

    public void endEvent()
    {
        betweenEvent = false;
        IsInputEnabled = true;
    }
}
