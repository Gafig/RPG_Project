using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControllerScript : MonoBehaviour {

    public Animator anim;
    public int currentFloor = 1;
    FloorGenerator floorGenerator;
    public Image fade;
    private GameObject player;
    private Rigidbody playerRB;
    [SerializeField]
    private float totalStep;
    private Vector3 playerLastPos;
    
    void Start () {
        currentFloor = 1;
        /***/floorGenerator = GameObject.Find("FloorGenerator").GetComponent(typeof(FloorGenerator)) as FloorGenerator;
        StartCoroutine(generateFloor());
        anim.SetBool("IsGen", true);
        setPlayerPosition();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        totalStep = 0;
        playerLastPos = player.transform.position;
    }
	
	void Update () {
        Vector3 playerCurrPos = player.transform.position;
        if (playerCurrPos != playerLastPos)
            totalStep++;
        playerLastPos = playerCurrPos;
	}

    void setPlayerPosition()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(18, -1, -0.1f);
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
        floorGenerator.generate(3, 0);
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

    void randomEncounter()
    {
        if(totalStep >= 1000)
        {
            //disable key
            //changeScence
            //getMon
        }
    }
}

