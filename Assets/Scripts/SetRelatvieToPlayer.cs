using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRelatvieToPlayer : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameMasterController.instance.player;
	}
	
	// Update is called once per frame
	void Update () {
        player = GameMasterController.instance.player;
        if (player.transform.position.y > transform.root.position.y)
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        else
            GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
