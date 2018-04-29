using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setDepth : MonoBehaviour {

    public int depth;
	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().sortingOrder = depth;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
