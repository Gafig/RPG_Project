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

    List<Character> objListOrder = new List<Character>();

    List<Character> characterList = new List<Character>();
    List<BaseMonster> monsterList = new List<BaseMonster>();

    float sumHp = 0;
    float sumMaxHp = 0;

    float flee = 0;

    public List<Character> Queue;

    public GameObject clickedGameObject;

    public BaseMonster monsterSelected;

   

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
        currentSelection = 0;
        // Debug.Log("START");
        // player = GameBattleManager.instance.character.GetComponent<BaseMonster>();
        // monster = GameBattleManager.instance.dMons.GetComponent<BaseMonster>();
        atkBtn = GameObject.Find("Atk").GetComponent<Button>();
        spBtn = GameObject.Find("Sp").GetComponent<Button>();
        bagBtn = GameObject.Find("Bag").GetComponent<Button>();
        runBtn = GameObject.Find("Run").GetComponent<Button>();
        // atkBtn.onClick.AddListener(attackFunction);
        // spBtn.onClick.AddListener(spAttackFunction);
        // bagBtn.onClick.AddListener(bagFunction);
        // runBtn.onClick.AddListener(runFunction);
		

    }

    void OnEnable()
    {
        // Debug.Log("enable ");
        // player = GameBattleManager.instance.character.GetComponent<BaseMonster>();
        // monster = GameBattleManager.instance.dMons.GetComponent<BaseMonster>();
        queue();
        // GameBattleManager.instance.charList[0].SetActive(true);
        // characterList[0].GetComponent<SpriteRenderer>().enabled = true;
        

        // for(int i = 0; i < characterList.Count ; i++){
        //     sumHp += characterList[i].getHP();
            // Debug.Log("HHHHHPPPPPP" + characterList[i].getHP());
		// }


    }

    // Update is called once per frame
    void Update()
    {

        if (objListOrder.Count == 0)
        {
            Debug.Log("enter queue");
            queue();
        }
        else
        {
            // objListOrder.RemoveAt(0);
            Debug.Log("battle");
        }

    }

    public void queue()
    {

        // objListOrder = new List<Character>();
        // objListOrder.Add(player);
		
		for(int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++){
        	objListOrder.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
            monsterList.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
		}
		for(int i = 0; i < GameBattleManager.instance.charList.Count; i++){
        	objListOrder.Add(GameBattleManager.instance.charList[i].GetComponent<Character>());
            characterList.Add(GameBattleManager.instance.charList[i].GetComponent<Character>());
            // Debug.Log(GameBattleManager.instance.charList[i].GetComponent<Character>());
		}



		// for(int i = 0 ; i < objListOrder.Count ; i++){
		// 	Debug.Log(objListOrder[i].PName +" "+ objListOrder[i].SpeedStat);
		// }

        Debug.Log("COUNT " + objListOrder.Count);
        objListOrder.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        objListOrder.Reverse();
        characterQueue();
        monsterQueue();


        // Debug.Log(objListOrder[0] + "   NEXT  " + objListOrder[1]);
		// for(int i = 0 ; i < objListOrder.Count ; i++){
		// 	Debug.Log(objListOrder[i].PName +" "+ objListOrder[i].SpeedStat);
		// }
        // for(int i = 0 ; i < objListOrder.Count ; i++){
		//     Debug.Log(objListOrder[i] + "   NEXT  ");
        // }

    }

    public void characterQueue(){
        characterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        characterList.Reverse();
        for(int i = 0; i < characterList.Count ; i++){
            sumMaxHp += characterList[i].getMaxHP();
        }
        Debug.Log(characterList[0]+ " NNN " + characterList[1]+ " NNN "+ characterList[2]+ " NNN "+characterList[3]+ " NNN ");
        characterList[0].GetComponent<SpriteRenderer>().enabled = true;
        // for(int i = 0 ; i < characterList.Count ; i++){
		//     Debug.Log(characterList[i] + "   NEXT  ");
        // }
    }

    public void monsterQueue(){
        monsterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        monsterList.Reverse();
        // for(int i = 0 ; i < monsterList.Count ; i++){
		//     Debug.Log(monsterList[i] + "   NEXT  ");
        // }
    }

     public void clicked(GameObject click){
        clickedGameObject = click;
        Debug.Log("asdafwf" + clickedGameObject.GetComponent<BaseMonster>().getHP());
        monsterSelected = clickedGameObject.GetComponent<BaseMonster>();
    }


    public void attackFunction()
    {
        // Debug.Log("monster "+ monsterList[0].OnMouseDown());
        Debug.Log("Before MONSTER HP" + monsterList[0].getHP());
        monsterSelected.attacked(characterList[0].getAttackStat());
        Debug.Log("select " + monsterSelected.getHP());
        Debug.Log("CHAR ATK" + characterList[0].getAttackStat());
        // monster.attacked(character.getAttackStat());

        // monster.attacked(player.getAttackStat());

        characterList[0].attacked(monsterSelected.getAttackStat());
        
        characterList[currentSelection].GetComponent<SpriteRenderer>().enabled = false;
        currentSelection++;
        if(currentSelection > 3){
            currentSelection=0;
        }
        
        characterList[currentSelection].GetComponent<SpriteRenderer>().enabled = true;

        Debug.Log("after MONSTER HP" + monsterSelected.getHP());
        // Debug.Log("CHARACTER ATK" + characterList[0].getAttackStat());
        Debug.Log("atk");


    }

    public void spAttackFunction()
    {
        Debug.Log("sp");
    }

    public void bagFunction()
    {
        Debug.Log("bag");
    }

    public void runFunction()
    {
        Debug.Log("run");
        Debug.Log(" before FLEE" + sumHp);
        for(int i = 0; i < characterList.Count ; i++){
            sumHp += characterList[i].getHP();
            // Debug.Log("HHHHHPPPPPP" + characterList[i].getHP());
		}

        Debug.Log("PPPP1: " + flee);
        flee = ((sumHp/sumMaxHp)*100) - 20;
        // if(flee < 50){
        //     flee = (sumHp/sumMaxHp)*100;
        //     Debug.Log("CHANGEEEE");
        // }

        float fleeP = Random.Range(0.0f, 100f);

        
        // Debug.Log(sumMaxHp +": PPPP :" + fleeP);
        
       
       if(flee < 60 && flee >= 30){
           flee = ((sumHp/sumMaxHp)*100);
       }
       if(flee < 30 && flee > 0){
           flee = ((sumHp/sumMaxHp)*100) + 27;
       }
       if(flee <= 0){
           flee = 25;
       }
       Debug.Log("SUM HP : " + sumHp +" : flee : " + flee + " : sum maxhp : "+ sumMaxHp + " : fleeP : " + fleeP );

        if(flee > fleeP){
            Debug.Log("FLEEEEEE");
            endCombat();
        }
        else{
            Debug.Log("Can't fleeee");
        }

        Debug.Log(" after FLEE" + sumHp);

        sumHp = 0;
    }


    public void endCombat()
    {
        // Debug.Log("End combat event");
        GameMasterController.instance.endEvent();
        GameMasterController.instance.IsInputEnabled = true;
        followingCamera.SetActive(true);
        battleCamera.SetActive(false);
        // Debug.Log("Change camera back to dun");
        GameMasterController.instance.setPermanantUI(true);
        // Debug.Log("setPermanantUI(true)");

        // Debug.Log(objListOrder[0] + "   NEXT  " + objListOrder[1]);
        objListOrder.Clear();
        // Debug.Log(objListOrder.Count);

		for(int i = 0; i < GameBattleManager.instance.dMonsList.Count ; i++){
        	Destroy(GameBattleManager.instance.dMonsList[i]);
		}
		// for(int i = 0; i < GameBattleManager.instance.charList.Count ; i++){
        // 	Destroy(GameBattleManager.instance.charList[i]);
		// }
		GameBattleManager.instance.dMonsList.Clear();
		// GameBattleManager.instance.charList.Clear();
        monsterList.Clear();
        characterList.Clear();

        
        sumMaxHp = 0;
        
    }

    public void reset(){
        
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