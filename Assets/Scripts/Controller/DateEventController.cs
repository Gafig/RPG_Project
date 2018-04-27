using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateEventController : MonoBehaviour {

    public static DateEventController instance;

    public GameObject[] events = new GameObject[5];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public int date = 1;
    public bool greeting = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Date = " + DateTimeController.instance.getDate());

        if (DateTimeController.instance.getDate() != date)
        {
            date = DateTimeController.instance.getDate();
            greeting = false;
        }

        if (!greeting)
        {
            greeting = true;
            events[date-2].transform.GetComponentInChildren<StartEvent>().trigger();
        } 
	}
}
