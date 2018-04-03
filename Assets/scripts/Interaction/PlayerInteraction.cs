using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	// Use this for initialization
    private MoveAround moveAround;
    public GameObject focusObject;

    public static PlayerInteraction instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        moveAround = gameObject.GetComponent<MoveAround>();
	}
	
	void Update () {
		
	}

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, moveAround.lastVelocity, out hit, 0.6f))
        {
            if (hit.collider.tag.Equals("interactable"))
            {
                if (focusObject != hit.transform.gameObject)
                    Debug.Log("Found an object - distance: " + hit.distance);
                focusObject = hit.transform.gameObject;
                focusAtObject();
            }
        }
        else
        {
            focusObject = null;
        }
    }

    private void focusAtObject()
    {
        if (focusObject == null)
            return;
        focusObject.GetComponent<InteractableObject>().focus();
    }
}
