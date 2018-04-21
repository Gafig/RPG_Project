﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleManager : MonoBehaviour {


	public GameObject battleCamera;
	public GameObject followingCamera;
	// public GameObject player;
	public List<BaseMonster> allMonster = new List<BaseMonster>();
	public List<MonsterMoves> allMoves = new List<MonsterMoves> ();

	public Party party;
	
	public BaseMonster mons;
	public Transform defencePodium;
	public Transform defencePodium1;
	public Transform defencePodium2;
	public Transform attackPodium;
	public GameObject emptyMons;
	public GameObject emptyMons1;
	public GameObject emptyMons2;
	public GameObject emptyCharacter;
	public GameObject dMons;
	public GameObject character;
	public Sprite sprite;

	public static GameBattleManager instance;

	public List<GameObject> dMonsList;
	 
	private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		followingCamera.SetActive (true);
		battleCamera.SetActive (false);

		
		character = Instantiate (emptyCharacter, attackPodium.transform.position, Quaternion.identity) as GameObject; 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterBattle(Rarity rarity){

		if(rarity == Rarity.Common){
			dMons = Instantiate (emptyMons, defencePodium.transform.position, Quaternion.identity) as GameObject;
		}
		if(rarity == Rarity.Rare){
			dMons = Instantiate (emptyMons1, defencePodium.transform.position, Quaternion.identity) as GameObject;
		}
		if(rarity == Rarity.VeryRare){
			dMons = Instantiate (emptyMons2, defencePodium.transform.position, Quaternion.identity) as GameObject;
		}
		dMonsList.Add(dMons);
		Debug.Log("DMONSLIST" + dMonsList);

		GameMasterController.instance.setPermanantUI(false);
        // Debug.Log("setPermanantUI(false)");

		// Debug.Log(GetMonsterByRarity(rarity));

		BaseMonster battleMonster = GetRandomMonsterFromList(GetMonsterByRarity(rarity));
		
		Vector3 monsLocalPos = new Vector3 (0, 1, 0);
		dMons.transform.parent = defencePodium;
		dMons.transform.localPosition = monsLocalPos;

		character.transform.parent = attackPodium;
		character.transform.localPosition = monsLocalPos;

		// BaseMonster tempMons = dMons.AddComponent<BaseMonster> () as BaseMonster;
		// tempMons.Addmember(battleMonster);
		// Debug.Log(battleMonster);
		// tempMons = battleMonster;

		dMons.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;
		// var type = Types.GetType("will2");
		// dMons.AddComponent<will2>();
		
		// Debug.Log(dMons);
		// Debug.Log(battleMonster.image);

		// Destroy(dMons);
	
		// bm.ChangeMenu (BattleMenu.Selection);
		
	}

	public List<BaseMonster> GetMonsterByRarity(Rarity rarity){
		List<BaseMonster> returnMonster = new List<BaseMonster> ();
		foreach (BaseMonster Monster in allMonster) {
			if (Monster.rarity == rarity)
				returnMonster.Add (Monster);
		}
		// Debug.Log(rarity);
		// Debug.Log("returnMonster" + returnMonster);

		return returnMonster;
		
	} 

	public BaseMonster GetRandomMonsterFromList(List<BaseMonster> monsList){
		GameObject gameObject = new GameObject("Monster");
    	mons = gameObject.AddComponent<BaseMonster>();
		int monsIndex = (int) Random.Range (0, monsList.Count - 1);
		mons = monsList[monsIndex];
		return mons;
	}
}


[System.Serializable]

public class MonsterMoves{
	string Name;
	public MoveType category;
	public int PP;
	public float Power;
	public float accuracy;
}


// poison, confuse, parasite
public enum MoveType{
	Physical,
	Special,
	Status
}