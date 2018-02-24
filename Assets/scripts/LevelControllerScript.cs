using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour {

    public Animator anim;

    FloorGenerator floorGenerator;
    // Use this for initialization
    void Start () {
        floorGenerator = GameObject.Find("FloorGenerator").GetComponent(typeof(FloorGenerator)) as FloorGenerator;
        StartCoroutine(generateFloor());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator generateFloor()
    {
        floorGenerator.generate(5);
        yield return new WaitUntil(() => floorGenerator.isFinished());
        anim.SetBool("IsGen", floorGenerator.isFinished());
        Debug.Log("Done Generating");
    }
}
