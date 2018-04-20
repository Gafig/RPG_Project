using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour {

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

 
	// Use this for initialization
	void Start () {
		currentSelection = 0;
		// fightT = fight.text;
		// bagT = bag.text;
		// monsterT = monster.text;
		// runT = run.text;
		// moveOT = moveO.text;
		// moveTT = moveT.text;
		// moveTHT = moveTH.text;
		// moveFT = moveF.text;
		atkBtn = GameObject.Find("AtkBtn").GetComponent<Button>();
		spBtn = GameObject.Find("SpBtn").GetComponent<Button>();
		bagBtn = GameObject.Find("BagBtn").GetComponent<Button>();
		runBtn = GameObject.Find("RunBtn").GetComponent<Button>();
		atkBtn.onClick.AddListener(attackFunction);
		spBtn.onClick.AddListener(spAttackFunction);
		bagBtn.onClick.AddListener(bagFunction);
		runBtn.onClick.AddListener(runFunction);
		
        

	}

	public void attackFunction(){
		Debug.Log("atk");
		
	}

	public void spAttackFunction(){
		Debug.Log("sp");				
	}

	public void bagFunction(){
		Debug.Log("bag");
	}

	public void runFunction(){
		Debug.Log("run");
		endCombat();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("z")){
			endCombat();
		}
		
		
	}

	public void endCombat(){
		Debug.Log("End combat event");
		GameMasterController.instance.endEvent();
		GameMasterController.instance.IsInputEnabled = true;
		followingCamera.SetActive(true);
        battleCamera.SetActive(false);
		Debug.Log("Change camera back to dun");
		GameMasterController.instance.setPermanantUI(true);
		Debug.Log("setPermanantUI(true)");
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


public enum BattleMenu{
	Selection,
	Special,
	Bag,
	Attack,
	Info
}