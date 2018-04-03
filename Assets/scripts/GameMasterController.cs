using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterController : MonoBehaviour {

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
        triggerEvents();
	}

    private void triggerEvents()
    {
        if(currentEvents.Count == 0)
            return;
        if (!betweenEvent)
        {
            Debug.Log(currentEvents.Count);
            Event e = currentEvents.Dequeue();
            Debug.Log(currentEvents.Count);
            e.trigger();
        }
    }

    public void startEvents(Event[] events)
    {
        
        if (betweenEvent)
            return;
        Debug.Log("Start");
        foreach (Event e in events)
        {
            currentEvents.Enqueue(e);
        }
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
        //StartCoroutine(sleep());
    }

    IEnumerator sleep()
    {
        yield return new WaitForSeconds(0.1f);
        betweenEvent = false;
        IsInputEnabled = true;
    }

    public void setLastDoorID(string doorId)
    {
        lastDoorID = doorId;
    }

    public void setPlayerToTheLastDoor()
    {
        PlayerSpawner[] spawners = GameObject.FindObjectsOfType<PlayerSpawner>();
        //Debug.Log(spawners.Length);
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
