using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour {
    [SerializeField]
    Sprite withSword;
    [SerializeField]
    Sprite withoutSword;

	// Use this for initialization
	void Start () {
        setSprite();
	}
	
	// Update is called once per frame
	void Update () {
        setSprite();
    }

    void setSprite()
    {
        if (FlagController.instance.SwordFlag)
            GetComponent<SpriteRenderer>().sprite = withoutSword;
        else
            GetComponent<SpriteRenderer>().sprite = withSword;
    }
}
