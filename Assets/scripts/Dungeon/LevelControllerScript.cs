using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControllerScript : MonoBehaviour {

    public GameObject battleCamera;
	public GameObject followingCamera;
    public GameBattleManager gameBattleManager; //
    public Animator anim;
    public int currentFloor;
    FloorGenerator floorGenerator;
    public Image fade;
    private GameObject player;
    private Rigidbody playerRB;

    [SerializeField]
    private int stepNeed;

    [SerializeField]
    private float totalStep;
    private Vector3 playerLastPos;
    private bool isDone = false;

    public EventHandler interaction;
    public bool hasInteracted;
    [SerializeField]
    bool needPress = true;
    
    void Start () {
        currentFloor = 0;
        /***/floorGenerator = GameObject.Find("FloorGenerator").GetComponent(typeof(FloorGenerator)) as FloorGenerator;
        player = GameObject.FindGameObjectWithTag("Player");
        //setPlayerPosition();
        StartCoroutine(generateFloor());
        playerRB = player.GetComponent<Rigidbody>();
        totalStep = 0;
        playerLastPos = player.transform.position;

        interaction = gameObject.GetComponent<EventHandler>();
        hasInteracted = false;

    }
	
	void Update () {
        Vector3 playerCurrPos = player.transform.position;
        if (playerCurrPos != playerLastPos)
            totalStep++;
        playerLastPos = playerCurrPos;
        randomEncounter();
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
        //StartCoroutine(FadeIn());
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

        if (totalStep >= stepNeed)
        {
             //disable key
            //changeScence
            //getMon
            Debug.Log("Start combat");
            // followingCamera.SetActive(false);
            // battleCamera.SetActive(true);
            react();
            // if (!hasInteracted)
            // {
            //     hasInteracted = true;
            //     // Debug.Log("Start combat");
            //     react();
            // }
            
            checkInteract();
        }
        
        Debug.Log (totalStep);
    }


    public void react()
    {
        interaction = gameObject.GetComponent<EventHandler>();
        if(interaction == null)
        {
            Debug.Log("Error");
            return;
        }
        interaction.triggerEvents();
    }

    private void checkInteract()
    {
        if (!GameMasterController.instance.betweenEvent){
            hasInteracted = false;
            Debug.Log("End combat");
            
        }
        totalStep = 0;

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

