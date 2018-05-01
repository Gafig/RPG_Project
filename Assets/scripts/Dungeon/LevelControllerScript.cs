using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControllerScript : MonoBehaviour
{

    public Flight flight;
    private int counttemp = 0;
    public Animator anim;
    public GameBattleManager gm;
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

    void Start()
    {
        currentFloor = 0;
        /***/
        floorGenerator = GameObject.Find("FloorGenerator").GetComponent(typeof(FloorGenerator)) as FloorGenerator;
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameBattleManager").GetComponent<GameBattleManager>();
        //setPlayerPosition();
        StartCoroutine(generateFloor());
        //prepareNextFloor();
        playerRB = player.GetComponent<Rigidbody>();
        totalStep = 0;
        playerLastPos = player.transform.position;

        interaction = gameObject.GetComponent<EventHandler>();
        hasInteracted = false;

    }

    void Update()
    {
        Vector3 playerCurrPos = player.transform.position;
        if (playerCurrPos != playerLastPos)
            totalStep++;
        playerLastPos = playerCurrPos;
        randomEncounter();
    }

    void setPlayerPosition()
    {
        player.transform.position = new Vector3(18, -2, -0.1f);
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
        if (currentFloor > 4)
            currentFloor = 4;
        if (currentFloor < 0)
            currentFloor = 0;
        FlagController.instance.dunTolevel[currentFloor] = true;
        //setPlayerPosition();
        StartCoroutine(generateFloor());
        //StartCoroutine(FadeIn());
        //GameMasterController.instance.IsInputEnabled = true;
    }

    IEnumerator generateFloor()
    {
        floorGenerator.generate(3, currentFloor);
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
        GameMasterController.instance.IsInputEnabled = true;
    }

    void randomEncounter()
    {

        if (totalStep >= stepNeed)
        {

            counttemp++;
            //disable key
            //changeScence
            //getMon

            if (BattleManager.instance.isWilliamAlive)
            {
                react();
                BattleManager.instance.flight = flight;
                for (int i = 0; i < 3; i++)
                {
                    float p = Random.Range(0.0f, 100.0f);
                    RandomMonster(p, i);
                }
                if (gm.william == null)
                {
                    gm.PlayerBattle();
                }


                checkInteract();
            }
        }

        // Debug.Log (totalStep + "count " + counttemp);
    }

    public void RandomMonster(float p, int pos)
    {
        if (p > 0 && p <= 20)
        {
            if (gm != null)
            {
                gm.EnterBattle(Rarity.VeryRare, pos);
            }
        }
        if (p > 20 && p <= 55)
        {
            if (gm != null)
            {
                gm.EnterBattle(Rarity.Rare, pos);
            }
        }

        if (p > 55 && p <= 100)
        {
            if (gm != null)
            {
                gm.EnterBattle(Rarity.Common, pos);
            }
        }

    }

    public void react()
    {

        GameMasterController.instance.IsInputEnabled = false;
        // Debug.Log("Start combat");
        interaction = gameObject.GetComponent<EventHandler>();
        if (interaction == null)
        {
            Debug.Log("Error");
            return;
        }
        interaction.triggerEvents();
        // gameObject.GetComponent<BattleManager>().setState(0);
        // GameObject battleCam = GameObject.Find("BattleCamera");

    }

    private void checkInteract()
    {
        if (!GameMasterController.instance.betweenEvent)
        {
            hasInteracted = false;
            Debug.Log("betweenEvent?");
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

