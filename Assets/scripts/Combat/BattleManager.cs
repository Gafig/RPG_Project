using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{

    private float prob;

    public GameObject battleCamera;
    public GameObject followingCamera;

    public Party party;
    public BattleMenu currentMenu;

    [Header("Selection")]
    public GameObject selectionMenu;
    public GameObject selectionAttack;
    public GameObject selectionSpecial;
    public GameObject selectionItem;
    public TextMeshProUGUI attack;
    private string attackT;
    public TextMeshProUGUI special;
    private string specialT;
    public TextMeshProUGUI bag;
    private string bagT;

    [Header("Misc")]
    public int currentSelection;
    public Button atkBtn;
    public Button spBtn;
    public Button bagBtn;
    public Button runBtn;

    Character player;
    Character monster;
    public static BattleManager instance;

    public bool isBattleEnd = false;

    List<Character> objListOrder = new List<Character>();

    List<Character> characterList = new List<Character>();
    List<BaseMonster> monsterList = new List<BaseMonster>();
    List<BaseMonster> monsterSelect;

    float sumHp = 0;
    float sumMaxHp = 0;

    float flee = 0;

    public List<Character> Queue;

    public GameObject clickedGameObject;

    public BaseMonster monsterSelected;

    public int attackStat;

    public Flight flight;

    public GameObject select1;
    public GameObject select2;
    public GameObject select3;

    public TextMeshProUGUI msg;
    public TextMeshProUGUI monsterName1;
    public TextMeshProUGUI monsterName2;
    public TextMeshProUGUI monsterName3;

    public Slider monsterHealth1;
    public Slider monsterHealth2;
    public Slider monsterHealth3;


    public bool isStart = true;


    // int STATE_EMPTY = -1;
    // int STATE_QUEUE = 0;
    // int STATE_SORT = 1;
    // int STATE_SELECTED_MONSTER = 2;
    // public int currentState = -1;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start()
    {

        atkBtn = GameObject.Find("Atk").GetComponent<Button>();
        spBtn = GameObject.Find("Sp").GetComponent<Button>();
        runBtn = GameObject.Find("Run").GetComponent<Button>();
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);


    }

    void OnEnable()
    {


    }

    // Update is called once per frame
    void Update()
    {

        // perform something
        if (objListOrder.Count == 0)
        {
            Debug.Log("enter queue");
            queue();
        }

        if (monsterSelected == null)
        {
            monsterSelected = monsterList[0];
            // Debug.Log("monsterSeelcted");
            // Debug.Log("000 " + monsterList[0]);
            select1.SetActive(false);
            select2.SetActive(false);
            select3.SetActive(false);
            monsterSelected.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }



        if (isStart)
        {
            monsterName1.text = GameBattleManager.instance.dMonsList[0].GetComponent<BaseMonster>().getPName();
            monsterName2.text = GameBattleManager.instance.dMonsList[1].GetComponent<BaseMonster>().getPName();
            monsterName3.text = GameBattleManager.instance.dMonsList[2].GetComponent<BaseMonster>().getPName();
            monsterHealth1.maxValue = GameBattleManager.instance.dMonsList[0].GetComponent<BaseMonster>().getMaxHP();
            monsterHealth1.value = monsterHealth1.maxValue;

            monsterHealth2.maxValue = GameBattleManager.instance.dMonsList[1].GetComponent<BaseMonster>().getMaxHP();
            monsterHealth2.value = monsterHealth2.maxValue;

            monsterHealth3.maxValue = GameBattleManager.instance.dMonsList[2].GetComponent<BaseMonster>().getMaxHP();
            monsterHealth3.value = monsterHealth3.maxValue;
            // Debug.Log("HPP " + monsterHealth1.value);
            isStart = false;
        }


    }

    public void queue()
    {

        for (int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++)
        {

            Debug.Log(GameBattleManager.instance.dMonsList[i]);
            objListOrder.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
            monsterList.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
        }

        monsterSelect = new List<BaseMonster>(monsterList);

        for (int i = 0; i < GameBattleManager.instance.charList.Count; i++)
        {
            if (GameBattleManager.instance.charList[i].GetComponent<Character>().isAlive())
            {
                // Debug.Log("HP "+ GameBattleManager.instance.charList[i].GetComponent<Character>().getHP());
                objListOrder.Add(GameBattleManager.instance.charList[i].GetComponent<Character>());
                characterList.Add(GameBattleManager.instance.charList[i].GetComponent<Character>());
            }
            // Debug.Log(GameBattleManager.instance.charList[i].GetComponent<Character>());
        }

        if (objListOrder.Count != 0)
        {
            objListOrder.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
            objListOrder.Reverse();
            if (characterList.Count != 0 && monsterList.Count != 0)
            {
                characterQueue();
                monsterQueue();
            }

        }

        currentSelection = 0;


    }

    public void characterQueue()
    {
        characterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        characterList.Reverse();
        for (int i = 0; i < characterList.Count; i++)
        {
            sumMaxHp += characterList[i].getMaxHP();
        }
        // Debug.Log(characterList[0] + " NNN " + characterList[1] + " NNN " + characterList[2] + " NNN " + characterList[3] + " NNN ");
        // characterList[0].GetComponent<SpriteRenderer>().enabled = true;

    }

    public void monsterQueue()
    {
        monsterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        monsterList.Reverse();

    }

    public void clicked(GameObject click)
    {
        clickedGameObject = click;
        Debug.Log("asdafwf" + clickedGameObject.GetComponent<BaseMonster>().getHP());
        monsterSelected = clickedGameObject.GetComponent<BaseMonster>();
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);
        monsterSelected.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("parent child " + monsterSelected.transform.parent.gameObject.transform.GetChild(0));
    }


    public void attackFunction()
    {
        if (currentSelection < characterList.Count)
        {
            msg.text = "now " + characterList[currentSelection].getPName() + " attacking " + monsterSelected.getPName();

            // monsterSelected.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(false);

            //stat atk player
            attackStat = characterList[currentSelection].getAttackStat();

            //player atk monster selected
            monsterSelected.attacked(attackStat);


            Debug.Log("max " + monsterHealth1.maxValue);
            Debug.Log("HPP2 " + monsterHealth1.value);
            Debug.Log("HPP3 " + monsterSelected.getHP());
            attackHelp();


        }

    }

    public void spAttackFunction()
    {
        if (currentSelection < characterList.Count)
        {
            Debug.Log("sp");

            attackStat = characterList[currentSelection].getSpAttackStat();
            attackHelp();
            //use mana
            characterList[currentSelection].useMana(20);


        }

    }


    private void attackHelp()
    {

        //update monster healthbar
        setHealthBar();

        //destroy monster that die
        if (monsterSelected.getHP() <= 0)
        {
            monsterList.Remove(monsterSelected);
            Destroy(monsterSelected.gameObject);
            // monsterSelected.GetComponent<BaseMonster>()
            if (monsterList.Count == 0)
            {
                endCombat();
                addMoney();
            }
            else
            {
                //monster atk player
                Debug.Log("Before " + currentSelection + " " + characterList[currentSelection].PName);
                characterList[currentSelection].attacked(monsterList[0].getAttackStat());

            }

        }


        //destroy player that die

        if (currentSelection < characterList.Count && characterList[currentSelection].getHP() <= 0)
        {
            if (characterList[currentSelection].getPName() == "william")
            {
                Debug.Log("W die");
                // endCombat();
            }
            // Destroy(characterList[currentSelection].gameObject);
            else
            {
                characterList[currentSelection].gameObject.SetActive(false);
                characterList.Remove(characterList[currentSelection]);

            }
            // Debug.Log("coutnt after dead " + characterList.Count);
        }
        //die all end battle
        if (!isBattleEnd)
        {

            // characterList[currentSelection].GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("current " + characterList[currentSelection].PName);
            currentSelection++;
            if (currentSelection > characterList.Count - 1)
            {
                currentSelection = 0;
            }
            Debug.Log("Next " + characterList[currentSelection].PName);

            // characterList[currentSelection].GetComponent<SpriteRenderer>().enabled = true;
            //change player active

        }


    }

    public void setHealthBar()
    {
        if (monsterHealth1 == monsterSelected.transform.parent.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Slider>())
        {
            if (monsterSelected.getHP() <= 0)
            {
                monsterHealth1.gameObject.SetActive(false);
            }
            monsterHealth1.value = monsterSelected.getHP();
        }
        if (monsterHealth2 == monsterSelected.transform.parent.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Slider>())
        {
            if (monsterSelected.getHP() <= 0)
            {
                monsterHealth2.gameObject.SetActive(false);
            }
            monsterHealth2.value = monsterSelected.getHP();
        }
        if (monsterHealth3 == monsterSelected.transform.parent.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Slider>())
        {
            if (monsterSelected.getHP() <= 0)
            {
                monsterHealth3.gameObject.SetActive(false);
            }
            monsterHealth3.value = monsterSelected.getHP();
        }
    }

    public void runFunction()
    {

        for (int i = 0; i < characterList.Count; i++)
        {
            sumHp += characterList[i].getHP();
        }


        flee = ((sumHp / sumMaxHp) * 100) - 20;
        Debug.Log("PPPP1: " + flee);

        float fleeP = Random.Range(0.0f, 100f);

        if (flee < 60 && flee >= 30)
        {
            flee = ((sumHp / sumMaxHp) * 100);
        }
        if (flee < 30 && flee > 0)
        {
            flee = ((sumHp / sumMaxHp) * 100) + 27;
        }
        if (flee <= 0)
        {
            flee = 25;
        }
        // Debug.Log("SUM HP : " + sumHp + " : flee : " + flee + " : sum maxhp : " + sumMaxHp + " : fleeP : " + fleeP);

        if (flee > fleeP)
        {
            // Debug.Log("FLEEEEEE");
            endCombat();
        }
        else
        {
            Debug.Log("Can't fleeee");
        }

        // Debug.Log(" after FLEE" + sumHp);

        sumHp = 0;
    }


    public void endCombat()
    {
        isBattleEnd = true;
        flight.triggerNextEvent();
        GameMasterController.instance.IsInputEnabled = true;
        followingCamera.SetActive(true);
        battleCamera.SetActive(false);
        GameMasterController.instance.setPermanantUI(true);
        objListOrder.Clear();
        for (int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++)
        {
            Destroy(GameBattleManager.instance.dMonsList[i]);
        }
        GameBattleManager.instance.dMonsList.Clear();

        monsterList.Clear();
        characterList.Clear();
        // Debug.Log(characterList);
        isStart = true;

        monsterHealth1.gameObject.SetActive(true);
        monsterHealth2.gameObject.SetActive(true);
        monsterHealth3.gameObject.SetActive(true);

        Debug.Log("END COMBAT");

        sumMaxHp = 0;

    }

    public void addMoney()
    {
        Debug.Log("MONEY");
    }

    public void reset()
    {
        sumHp = 0;
        flee = 0;
        sumMaxHp = 0;
    }

}


public enum BattleMenu
{
    Selection,
    Special,
    Bag,
    Attack,
    Info
}