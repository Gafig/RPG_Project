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
        player = GameObject.FindGameObjectWithTag("Player");
        //setPlayerPosition();
        StartCoroutine(generateFloor());
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
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        Fade.instance.fadeOut();
        yield return new WaitUntil(() => !Fade.instance.isFading);
        prepareNextFloor();
    }

    IEnumerator FadeIn()
    {
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
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

