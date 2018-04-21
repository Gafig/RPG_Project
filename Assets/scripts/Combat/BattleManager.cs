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

    public List<Character> Queue;

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
        Debug.Log("enable ");
        // player = GameBattleManager.instance.character.GetComponent<BaseMonster>();
        // monster = GameBattleManager.instance.dMons.GetComponent<BaseMonster>();
        queue();

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
		}
		for(int i = 0; i < GameBattleManager.instance.charList.Count; i++){
        	objListOrder.Add(GameBattleManager.instance.charList[i].GetComponent<BaseMonster>());
		}
		for(int i = 0 ; i < objListOrder.Count ; i++){
			Debug.Log(objListOrder[i].PName +" "+ objListOrder[i].SpeedStat);
		}
        Debug.Log(objListOrder.Count);
        objListOrder.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        objListOrder.Reverse();
        Debug.Log(objListOrder[0] + "   NEXT  " + objListOrder[1]);
		for(int i = 0 ; i < objListOrder.Count ; i++){
			Debug.Log(objListOrder[i].PName +" "+ objListOrder[i].SpeedStat);
		}
		Debug.Log(objListOrder[0] + "   NEXT  " + objListOrder[1] +  "   NEXT  " + objListOrder[2] + "   NEXT  " + objListOrder[3]);

    }



    public void attackFunction()
    {

        monster.attacked(player.getAttackStat());

        Debug.Log(monster.getMaxHP());
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
        endCombat();
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
		for(int i = 0; i < GameBattleManager.instance.charList.Count ; i++){
        	Destroy(GameBattleManager.instance.charList[i]);
		}
		GameBattleManager.instance.dMonsList.Clear();
		GameBattleManager.instance.charList.Clear();
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