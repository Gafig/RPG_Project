using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

  	public GameObject battleCamera;
	public GameObject followingCamera;
	public BattleMenu currentMenu;

	[Header("Selection")]
	public GameObject selectionMenu;
	public GameObject selectionInfo;
	public Text selectionInfoText;
	public Text fight;
	private string fightT;
	public Text bag;
	private string bagT;
	public Text monster;
	private string monsterT;
	public Text run;
	private string runT;

	[Header("Moves")]
	public GameObject movesMenu;
	public GameObject movesDatail;
	public Text PP;
	public Text pType;
	public Text moveO;
	private string moveOT;
	public Text moveT;
	private string moveTT;
	public Text moveTH;
	private string moveTHT;
	public Text moveF;
	private string moveFT;

	[Header("Info")]
	public GameObject InfoMenu;
	public Text InfoText;
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
		if(Input.GetKeyDown("z")){
			if(currentSelection < 4){
				currentSelection++;
			}
		}
		if(Input.GetKeyDown("x")){
			if(currentSelection > 0){
				currentSelection--;
			}	
		}
		if (currentSelection == 0)
			currentSelection = 1;
		
		if(currentSelection == 4){
			end = true;
		}

		Debug.Log(currentSelection);
		
		if(end){
			endCombat();
		}
		
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

		// switch (m) {

		// case BattleMenu.Selection:
		// 	selectionMenu.gameObject.SetActive (true);
		// 	selectionInfo.gameObject.SetActive (true);
		// 	movesMenu.gameObject.SetActive (false);
		// 	movesDatail.gameObject.SetActive (false);
		// 	InfoMenu.gameObject.SetActive (false);
		// 	break;
		// case BattleMenu.Fight:
		// 	selectionMenu.gameObject.SetActive (false);
		// 	selectionInfo.gameObject.SetActive (false);
		// 	movesMenu.gameObject.SetActive (true);
		// 	movesDatail.gameObject.SetActive (true);
		// 	InfoMenu.gameObject.SetActive (false);
		// 	break;
		
		// case BattleMenu.Info:
		// 	selectionMenu.gameObject.SetActive (false);
		// 	selectionInfo.gameObject.SetActive (false);
		// 	movesMenu.gameObject.SetActive (false);
		// 	movesDatail.gameObject.SetActive (false);
		// 	InfoMenu.gameObject.SetActive (true);
		// 	break;
		// }

	}
}


public enum BattleMenu{
	Selection,
	Monster,
	Bag,
	Fight,
	Info
}