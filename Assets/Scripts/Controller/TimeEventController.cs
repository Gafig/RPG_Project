using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventController : MonoBehaviour {

    public static TimeEventController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in transform)
        {
            StartEventByTime e = child.GetComponentInChildren<StartEventByTime>();
            if(e != null)
            {
                e.trigger();
            }
        }
    }
}
