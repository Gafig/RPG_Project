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

    [Header("Attack")]
    public GameObject attackMenu;
    public GameObject movesDatail;
    public TextMeshProUGUI PP;
    public TextMeshProUGUI pType;
    public TextMeshProUGUI moveO;
    private string moveOT;
    public TextMeshProUGUI moveT;
    private string moveTT;
    public TextMeshProUGUI moveTH;
    private string moveTHT;
    public TextMeshProUGUI moveF;
    private string moveFT;

    [Header("Bag")]
    public GameObject bagMenu;
    public TextMeshProUGUI item;
    private string itemT;



    [Header("Info")]
    public GameObject InfoMenu;
    public TextMeshProUGUI InfoText;

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
        // setState(STATE_QUEUE);

        // Debug.Log("START");
        // player = GameBattleManager.instance.character.GetComponent<BaseMonster>();
        // monster = GameBattleManager.instance.dMons.GetComponent<BaseMonster>();
        atkBtn = GameObject.Find("Atk").GetComponent<Button>();
        spBtn = GameObject.Find("Sp").GetComponent<Button>();
        runBtn = GameObject.Find("Run").GetComponent<Button>();
        // select1 = gameObject;
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);
        // atkBtn.onClick.AddListener(attackFunction);
        // spBtn.onClick.AddListener(spAttackFunction);
        // bagBtn.onClick.AddListener(bagFunction);
        // runBtn.onClick.AddListener(runFunction);


    }

    void OnEnable()
    {
        // setState(STATE_QUEUE);
        // Debug.Log("enable ");
        // player = GameBattleManager.instance.character.GetComponent<BaseMonster>();
        // monster = GameBattleManager.instance.dMons.GetComponent<BaseMonster>();
        // Debug.Log("ENABLE " + objListOrder.Count);

        // if (objListOrder.Count == 0)
        // {
        //     // Debug.Log("ENABLE");
        //     queue();
        // }
        // GameBattleManager.instance.charList[0].SetActive(true);
        // characterList[0].GetComponent<SpriteRenderer>().enabled = true;


        // for(int i = 0; i < characterList.Count ; i++){
        //     sumHp += characterList[i].getHP();
        // Debug.Log("HHHHHPPPPPP" + characterList[i].getHP());
        // }


    }

    // public void setState(int state)
    // {
    //     currentState = state;
    // }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("state" + currentState);
        // if (currentState == STATE_QUEUE)
        // {
        // perform something
        if (objListOrder.Count == 0)
        {
            Debug.Log("enter queue");
            queue();
        }
        // currentState = STATE_SELECTED_MONSTER; // change state to sort 




        // }
        // else if (currentState == STATE_SELECTED_MONSTER)
        // {
        // performe something;
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
            isStart = false;
        }


        // setState(STATE_EMPTY);
        // }

        // if william die
        for (int i = 0; i < characterList.Count - 1; i++)
        {
            if (characterList[i].getPName() == "william" && characterList[i].getHP() <= 0)
            {
                endCombat();
            }
        }



    }

    public void queue()
    {

        // objListOrder = new List<Character>();
        // objListOrder.Add(player);

        for (int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++)
        {

            Debug.Log(GameBattleManager.instance.dMonsList[i]);
            objListOrder.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
            monsterList.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
        }

        monsterSelect = new List<BaseMonster>(monsterList);

        // for (int i = 0; i < monsterSelect.Count; i++)
        // {
        //     Debug.Log("LOGGGGG " + monsterSelect[i].getPName());
        // }
        // Debug.Log("COUNT LIST " + GameBattleManager.instance.charList.Count);

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



        // for(int i = 0 ; i < objListOrder.Count ; i++){
        // 	Debug.Log("QQQQQQQQ"+objListOrder[i].PName +" "+ objListOrder[i].SpeedStat);
        // }

        // Debug.Log("COUNT " + objListOrder.Count);
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

        // Debug.Log("AAAA " + objListOrder.Count);


        currentSelection = 0;


        // Debug.Log(objListOrder[0] + "   NEXT  " + objListOrder[1]);
        // for(int i = 0 ; i < objListOrder.Count ; i++){
        // 	Debug.Log(objListOrder[i].PName +" "+ objListOrder[i].SpeedStat);
        // }
        // for(int i = 0 ; i < objListOrder.Count ; i++){
        //     Debug.Log(objListOrder[i] + "   NEXT  ");
        // }

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
        characterList[0].GetComponent<SpriteRenderer>().enabled = true;
        // for(int i = 0 ; i < characterList.Count ; i++){
        //     Debug.Log(characterList[i] + "   NEXT  ");
        // }
    }

    public void monsterQueue()
    {
        monsterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        monsterList.Reverse();
        // Debug.Log("monslist " + monsterList.Count);
        // for(int i = 0 ; i < monsterList.Count ; i++){
        //     Debug.Log(monsterList[i] + "   NEXT  ");
        // }
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

        // Debug.Log("monster "+ monsterList[0].OnMouseDown());
        // Debug.Log("Before MONSTER HP" + monsterList[0].getHP());
        // Debug.Log("LIST COUNT " + objListOrder.Count);
        // Debug.Log("START " + currentSelection);
        // Debug.Log("WHO " + characterList[currentSelection].PName);
        msg.text = characterList[currentSelection].getPName() + " attacking " + monsterSelected.getPName();
        attackStat = characterList[currentSelection].getAttackStat();
        //player atk monster selected
        monsterSelected.attacked(attackStat);
        attackHelp();


        // Debug.Log("LIST COUNT2 " + objListOrder.Count);

    }

    public void spAttackFunction()
    {

        Debug.Log("sp");

        attackStat = characterList[currentSelection].getSpAttackStat();
        attackHelp();
        //use mana
        characterList[currentSelection].useMana(20);


    }


    private void attackHelp()
    {

        // Debug.Log("select " + monsterSelected.getHP());
        // Debug.Log("CHAR ATK" + characterList[0].getAttackStat());
        // monster.attacked(character.getAttackStat());

        // monster.attacked(player.getAttackStat());

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
                Debug.Log("ATKKKK " + currentSelection + " " + characterList[currentSelection].PName);
                characterList[currentSelection].attacked(monsterList[0].getAttackStat());

                //animation test
                // monsterList[0].GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);


                //destroy player that die
                if (characterList[currentSelection].getHP() <= 0)
                {
                    // Destroy(characterList[currentSelection].gameObject);
                    characterList[currentSelection].gameObject.SetActive(false);
                    characterList.Remove(characterList[currentSelection]);
                    Debug.Log("coutnt after dead " + characterList.Count);
                }

            }
        }
        //die all end battle
        if (!isBattleEnd)
        {

            // Debug.Log("current char " + characterList[currentSelection].name);
            //change player active
            characterList[currentSelection].GetComponent<SpriteRenderer>().enabled = false;
            currentSelection++;
            if (currentSelection > characterList.Count - 1)
            {
                currentSelection = 0;
            }
            // Debug.Log("current char " + characterList[currentSelection].name);
            
            characterList[currentSelection].GetComponent<SpriteRenderer>().enabled = true;
            //change player active

            // Debug.Log("after MONSTER HP" + monsterSelected.getHP());
            // Debug.Log("CHARACTER ATK" + characterList[0].getAttackStat());
            // Debug.Log("atk");

        }



        // for (int i = 0; i < monsterList.Count; i++)
        // {
        //     Debug.Log("Monster : " + monsterList[i]);
        // }
        // Debug.Log("COUNTTT" + monsterList.Count);



        //destroy player that die
        // if (characterList[currentSelection].getHP() <= 0)
        // {
        //     Destroy(characterList[currentSelection].gameObject);
        //     characterList.Remove(characterList[currentSelection]);
        //     Debug.Log("coutnt after dead " + characterList.Count);
        // }

        // Debug.Log("is END" + isBattleEnd);
    }

    // public void bagFunction()
    // {
    //     Debug.Log("bag");
    // }

    public void runFunction()
    {
        // Debug.Log("run");
        // Debug.Log(" before FLEE" + sumHp);
        for (int i = 0; i < characterList.Count; i++)
        {
            sumHp += characterList[i].getHP();
            // Debug.Log("HHHHHPPPPPP" + characterList[i].getHP());
        }


        flee = ((sumHp / sumMaxHp) * 100) - 20;
        Debug.Log("PPPP1: " + flee);
        // if(flee < 50){
        //     flee = (sumHp/sumMaxHp)*100;
        //     Debug.Log("CHANGEEEE");
        // }

        float fleeP = Random.Range(0.0f, 100f);


        // Debug.Log(sumMaxHp +": PPPP :" + fleeP);


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
        // Debug.Log("End combat event");
        flight.triggerNextEvent();
        GameMasterController.instance.IsInputEnabled = true;
        followingCamera.SetActive(true);
        battleCamera.SetActive(false);
        // Debug.Log("Change camera back to dun");
        GameMasterController.instance.setPermanantUI(true);
        // Debug.Log("setPermanantUI(true)");

        // Debug.Log(objListOrder[0] + "   NEXT  " + objListOrder[1]);
        objListOrder.Clear();
        // Debug.Log(objListOrder.Count);

        for (int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++)
        {
            Destroy(GameBattleManager.instance.dMonsList[i]);
        }
        GameBattleManager.instance.dMonsList.Clear();

        // GameBattleManager.instance.charList.Clear();
        // for(int i = 0; i < GameBattleManager.instance.charList.Count ; i++){
        // 	Destroy(GameBattleManager.instance.charList[i]);
        // }
        // GameBattleManager.instance.dMonsList.Clear();    
        // GameBattleManager.instance.charList.Clear();
        currentSelection = 0;
        monsterList.Clear();
        characterList.Clear();
        Debug.Log(characterList);
        isStart = true;
        Debug.Log("END COMBAT");
        // Debug.Log("LIST COUNT3 " + objListOrder.Count);

        // Debug.Log("LIST "+ characterList.Count);

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

    // public void ChangeMenu(BattleMenu m){
    // 	currentMenu = m;
    // 	Debug.Log (m);
    // 	currentSelection = 1;

    // 	switch (m) {

    // 	case BattleMenu.Selection:
    // 		selectionMenu.gameObject.SetActive (true);
    // 		selectionAttack.gameObject.SetActive (false);
    // 		selectionSpecial.gameObject.SetActive (false);
    // 		selectionItem.gameObject.SetActive (false);
    // 		break;

    // 	case BattleMenu.Attack:
    // 		selectionMenu.gameObject.SetActive (false);
    // 		selectionAttack.gameObject.SetActive (true);
    // 		selectionSpecial.gameObject.SetActive (false);
    // 		selectionItem.gameObject.SetActive (false);
    // 		break;

    // 	case BattleMenu.Special:
    // 		selectionMenu.gameObject.SetActive (false);
    // 		selectionAttack.gameObject.SetActive (false);
    // 		selectionSpecial.gameObject.SetActive (true);
    // 		selectionItem.gameObject.SetActive (false);
    // 		break;

    // 	case BattleMenu.Bag:
    // 		selectionMenu.gameObject.SetActive (false);
    // 		selectionAttack.gameObject.SetActive (false);
    // 		selectionSpecial.gameObject.SetActive (false);
    // 		selectionItem.gameObject.SetActive (true);
    // 		break;
    // 	}

    // }
}


public enum BattleMenu
{
    Selection,
    Special,
    Bag,
    Attack,
    Info
}