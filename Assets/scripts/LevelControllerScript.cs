using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControllerScript : MonoBehaviour {

    public Animator anim;
    public int currentFloor = 1;
    FloorGenerator floorGenerator;
    public Image fade;
    // Use this for initialization
    void Start () {
        currentFloor = 1;
        floorGenerator = GameObject.Find("FloorGenerator").GetComponent(typeof(FloorGenerator)) as FloorGenerator;
        StartCoroutine(generateFloor());
        anim.SetBool("IsGen", true);
        setPlayerPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void setPlayerPosition()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(18, -1, 0);
    }

    public void toNextLevel()
    {
        GameMasterController.IsInputEnabled = false;
        StartCoroutine(FadeOut());
    }

    void prepareNextFloor()
    {
        currentFloor++;
        StartCoroutine(generateFloor());
        setPlayerPosition();
        StartCoroutine(FadeIn());
        GameMasterController.IsInputEnabled = true;
    } 

    IEnumerator generateFloor()
    {
        floorGenerator.generate(1);
        yield return new WaitUntil(() => floorGenerator.isFinished());
    }

    IEnumerator FadeOut()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => fade.color.a == 1);
        prepareNextFloor();
    }

    IEnumerator FadeIn()
    {
        anim.SetBool("Fade", false);
        yield return new WaitUntil(() => fade.color.a == 0);
    }
}
