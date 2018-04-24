using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour {

	public GameObject battleCamera;
	public GameObject followingCamera;

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
	public TextMeshProUGUI run;
	private string runT;

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
	private bool end;

	[Header("Misc")]
	public int currentSelection;
 
	// Use this for initialization
	void Start () {
		end = false;
		currentSelection = 0;
		// fightT = fight.text;
		// bagT = bag.text;
		// monsterT = monster.text;
		// runT = run.text;
		// moveOT = moveO.text;
		// moveTT = moveT.text;
		// moveTHT = moveTH.text;
		// moveFT = moveF.text;

	}
	
	// Update is called once per frame
	void Update () {

		// if(Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)){
		// 	if(currentSelection < 4){
		// 		currentSelection++;
		// 	}
		// }

		// if(Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)){
		// 	if(currentSelection > 0){
		// 		currentSelection--;
		// 	}
		// }

		if(Input.GetKeyDown("z")){
			endCombat();
		}
		// if(Input.GetKeyDown("x")){
		// 	if(currentSelection > 0){
		// 		currentSelection--;
		// 	}	
		// }
		// if (currentSelection == 0)
		// 	currentSelection = 1;
		
		// if(currentSelection == 4){
		// 	end = true;
		// 	endCombat();
		// }

		// Debug.Log(currentSelection);
		
		// if(end){
		// 	endCombat();
		// }
		
		// switch (currentMenu) {
		// case BattleMenu.Fight:
		// 	switch (currentSelection) {
		// 	case 1:
		// 		moveO.text = "> " + moveOT;
		// 		moveT.text = moveTT;
		// 		moveTH.text = moveTHT;
		// 		moveF.text = moveFT;
		// 		break;
		// 	case 2:
		// 		moveO.text = moveOT;
		// 		moveT.text = "> " + moveTT;
		// 		moveTH.text = moveTHT;
		// 		moveF.text = moveFT;
		// 		break;
		// 	case 3:
		// 		moveO.text = moveOT;
		// 		moveT.text = moveTT;
		// 		moveTH.text = "> " + moveTHT;
		// 		moveF.text = moveFT;
		// 		break;
		// 	case 4:
		// 		moveO.text = moveOT;
		// 		moveT.text = moveTT;
		// 		moveTH.text = moveTHT;
		// 		moveF.text = "> " + moveFT;
		// 		break;
		// 	}
		// 	break;
		// case BattleMenu.Selection:
		// 	switch (currentSelection) {
		// 	case 1:
		// 		fight.text = "> " + fightT;
		// 		bag.text = bagT;
		// 		monster.text = monsterT;
		// 		run.text = runT;
		// 		break;
		// 	case 2:
		// 		fight.text = fightT;
		// 		bag.text = "> " + bagT;
		// 		monster.text = monsterT;
		// 		run.text = runT;				
		// 		break;
		// 	case 3:
				
		// 		fight.text = fightT;
		// 		bag.text = bagT;
		// 		monster.text = "> " + monsterT;
		// 		run.text = runT;
		// 		break;
		// 	case 4:
		// 		fight.text = fightT;
		// 		bag.text = bagT;
		// 		monster.text = monsterT;
		// 		run.text = "> " + runT;
	
		// 		break;
		// 	}
		// 	break;

		// }
		
	}

	public void endCombat(){
		GameMasterController.instance.endEvent();
		GameMasterController.instance.IsInputEnabled = true;
		followingCamera.SetActive(true);
        battleCamera.SetActive(false);
		Debug.Log("Change camera back to dun");
		end = false;
		currentSelection = 0;
	}

	public void ChangeMenu(BattleMenu m){
		currentMenu = m;
		Debug.Log (m);
		currentSelection = 1;

		switch (m) {

		case BattleMenu.Selection:
			selectionMenu.gameObject.SetActive (true);
			selectionAttack.gameObject.SetActive (false);
			selectionSpecial.gameObject.SetActive (false);
			selectionItem.gameObject.SetActive (false);
			break;

		case BattleMenu.Attack:
			selectionMenu.gameObject.SetActive (false);
			selectionAttack.gameObject.SetActive (true);
			selectionSpecial.gameObject.SetActive (false);
			selectionItem.gameObject.SetActive (false);
			break;

		case BattleMenu.Special:
			selectionMenu.gameObject.SetActive (false);
			selectionAttack.gameObject.SetActive (false);
			selectionSpecial.gameObject.SetActive (true);
			selectionItem.gameObject.SetActive (false);
			break;
		
		case BattleMenu.Bag:
			selectionMenu.gameObject.SetActive (false);
			selectionAttack.gameObject.SetActive (false);
			selectionSpecial.gameObject.SetActive (false);
			selectionItem.gameObject.SetActive (true);
			break;
		}

	}
}


public enum BattleMenu{
	Selection,
	Special,
	Bag,
	Attack,
	Info
}