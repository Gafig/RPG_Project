using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	public BattleMenu currentMenu;

	[Header("Selection")]
	public GameObject selectionMenu;
	public GameObject selectionInfo;
	public Text selectionInfoText;
	public Text fight;
	private string fightT;
	public Text bag;
	private string bagT;
	public Text pokemon;
	private string pokemonT;
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

	[Header("Misc")]
	public int currentSelection;

	// Use this for initialization
	void Start () {
		fightT = fight.text;
		bagT = bag.text;
		pokemonT = pokemon.text;
		runT = run.text;
		moveOT = moveO.text;
		moveTT = moveT.text;
		moveTHT = moveTH.text;
		moveFT = moveF.text;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if(currentSelection < 4){
				currentSelection++;
			}
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if(currentSelection > 0){
				currentSelection--;
			}	
		}

		Text first;
		string ft;
		Text second;
		string st;
		Text third;
		string tt;
		Text fourth;
		string fft;

		switch (currentMenu) {
		case BattleMenu.Fight:
			first = moveO;
			second = moveT;
			third = moveTH;
			fourth = moveF;

			ft = moveOT;
			st = moveTT;
			tt = moveTHT;
			ft = moveFT;
			break;
		case BattleMenu.Selection:
			first = fight;
			second = bag;
			third = pokemon;
			fourth = run;

			ft = fightT;
			st = bagT;
			tt = pokemonT;
			ft = runT;
			break;

		}
		
	}

	public void ChangeMenu(BattleMenu m){
		currentMenu = m;
		currentSelection = 1;
		switch (m) {

		case BattleMenu.Selection:
			selectionMenu.gameObject.SetActive (true);
			selectionInfo.gameObject.SetActive (true);
			movesMenu.gameObject.SetActive (false);
			movesDatail.gameObject.SetActive (false);
			InfoMenu.gameObject.SetActive (false);
			break;
		case BattleMenu.Fight:
			selectionMenu.gameObject.SetActive (false);
			selectionInfo.gameObject.SetActive (false);
			movesMenu.gameObject.SetActive (true);
			movesDatail.gameObject.SetActive (true);
			InfoMenu.gameObject.SetActive (false);
			break;
		
		case BattleMenu.Info:
			selectionMenu.gameObject.SetActive (false);
			selectionInfo.gameObject.SetActive (false);
			movesMenu.gameObject.SetActive (false);
			movesDatail.gameObject.SetActive (false);
			InfoMenu.gameObject.SetActive (true);
			break;
		}

	}
}


public enum BattleMenu{
	Selection,
	Pokemon,
	Bag,
	Fight,
	Info
}