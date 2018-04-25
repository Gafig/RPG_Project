using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterController : MonoBehaviour {

    public bool showTime = true;
    public bool IsInputEnabled = true;
    public bool betweenEvent = false;
    public Queue<Event> currentEvents;

    public GameObject player;
    public string lastDoorID;

    public static GameMasterController instance;

    private void Awake()
    {
        player = GameObject.Find("player");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        currentEvents = new Queue<Event>();
        player = GameObject.Find("Player");
    }
	
	void Update () {
        if(player == null)
            player = GameObject.Find("Player");
        triggerEvents();
	}

    private void triggerEvents()
    {
        if(currentEvents.Count == 0)
            return;
        if (!betweenEvent)
        {
            Event e = currentEvents.Dequeue();
            e.trigger();
        }
    }

    public void setShowTime(bool showTime)
    {
        this.showTime = showTime;
        GameObject go = GameObject.Find("PermanantUI");
        go.transform.FindChild("DateTimePanel").gameObject.SetActive(showTime);
    }

    public void setPermanantUI(bool set)
    {
        GameObject go = GameObject.Find("PermanantUI");
        for (int i = 0; i < go.transform.childCount; i++) {
            Transform t = go.transform.GetChild(i);
            t.gameObject.SetActive(set);
        }
    }

    public void startEvents(Event[] events)
    {
        
        if (betweenEvent)
            return;
        
        foreach (Event e in events)
        {
            currentEvents.Enqueue(e);
        }
    }

    public bool startEvent()
    {
        if (betweenEvent)
            return false;
        DateTimeController.instance.stopTime();
        betweenEvent = true;
        IsInputEnabled = false;
        return true;
    }

    public void endEvent()
    {
        betweenEvent = false;
        IsInputEnabled = true;
        DateTimeController.instance.startTime();
    }

    public void setLastDoorID(string doorId)
    {
        lastDoorID = doorId;
    }

    public void setPlayerToTheLastDoor()
    {
        PlayerSpawner[] spawners = GameObject.FindObjectsOfType<PlayerSpawner>();
        Debug.Log("spanwers = " + spawners.Length);
        foreach (PlayerSpawner sp in spawners)
        {
            //Debug.Log("Compare:" + sp.id + " to:" + lastDoorID + " Resuls:" + sp.id.Equals(lastDoorID));
            if (sp.id.Equals(lastDoorID))
            {
                Vector3 pos = sp.transform.position;
                player.transform.position = new Vector3(pos.x, pos.y, player.transform.position.z);
                player.GetComponent<MoveAround>().face(sp.facing);
            }
        }
    }
}
