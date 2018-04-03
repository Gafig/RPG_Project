using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControllerScript : MonoBehaviour {

    public Animator anim;
    public int currentFloor;
    FloorGenerator floorGenerator;
    public Image fade;
    private GameObject player;
    private Rigidbody playerRB;
    [SerializeField]
    private float totalStep;
    private Vector3 playerLastPos;
    private bool isDone = false;
    
    void Start () {
        currentFloor = 0;
        /***/floorGenerator = GameObject.Find("FloorGenerator").GetComponent(typeof(FloorGenerator)) as FloorGenerator;
        StartCoroutine(generateFloor());
        anim.SetBool("IsGen", true);
        player = GameObject.FindGameObjectWithTag("Player");
        setPlayerPosition();
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
        player.transform.position = new Vector3(17, -2, -0.1f);
        //player.transform.position = new Vector3(0, 0, -0.1f);
        //GameMasterController.instance.setPlayerToTheLastDoor();
    }

    public void toNextLevel()
    {
        Debug.Log("Prepare to move to " + currentFloor);
        GameMasterController.instance.IsInputEnabled = false;
        StartCoroutine(FadeOut());
    }

    void prepareNextFloor()
    {
        setPlayerPosition();
        StartCoroutine(generateFloor());
        StartCoroutine(FadeIn());
        GameMasterController.instance.IsInputEnabled = true;
    } 

    IEnumerator generateFloor()
    {
        floorGenerator.generate(1, currentFloor);
        yield return new WaitUntil(() => floorGenerator.isFinished());
        GameMasterController.instance.setPlayerToTheLastDoor();
        isDone = false;
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

    public void toLowerLevel()
    {
        if (!isDone)
        {
            currentFloor++;
            isDone = true;
        }
    }

    public void toHigherLevel()
    {
        if (!isDone)
        {
            Debug.Log("Up we go!");
            currentFloor--;
            isDone = true;
        }
    }
}

