using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, Interactable {

    public new string name;
    public Event interaction;

    public void react()
    {
        throw new NotImplementedException();
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
