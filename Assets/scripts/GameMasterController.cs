using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterController : MonoBehaviour {

    public bool IsInputEnabled = true;
    public bool betweenEvent = false;
    public Queue<Event> currentEvents;

    public static GameMasterController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        currentEvents = new Queue<Event>();
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
}
