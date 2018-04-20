using System.Collections;
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
	public Transform attackPodium;
	public GameObject emptyMons;
	public GameObject emptyCharacter;

	 

	// Use this for initialization
	void Start () {
		followingCamera.SetActive (true);
		battleCamera.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterBattle(Rarity rarity){

		GetMonsterByRarity(rarity);


		GameObject dMons = Instantiate (emptyMons, defencePodium.transform.position, Quaternion.identity) as GameObject; 
		GameObject character = Instantiate (emptyCharacter, attackPodium.transform.position, Quaternion.identity) as GameObject; 
		Vector3 monsLocalPos = new Vector3 (0, 1, 0);
		dMons.transform.parent = defencePodium;
		dMons.transform.localPosition = monsLocalPos;

		character.transform.parent = attackPodium;
		character.transform.localPosition = monsLocalPos;

		BaseMonster tempMons = dMons.AddComponent<BaseMonster> () as BaseMonster;
		// tempMons.Addmember(battleMonster);
		// tempMons = battleMonster;

		// dMons.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;

	
		// bm.ChangeMenu (BattleMenu.Selection);
		
	}

	public void GetMonsterByRarity(Rarity rarity){
		// List<BaseMonster> returnMonster = new List<BaseMonster> ();
		// foreach (BaseMonster Monster in allMonster) {
		// 	if (Monster.rarity == rarity)
		// 		returnMonster.Add (Monster);
		// }

		// return returnMonster;
		Debug.Log(rarity);
	} 

	public BaseMonster GetRandomMonsterFromList(List<BaseMonster> monsList){
		GameObject gameObject = new GameObject("Monster");
    	mons = gameObject.AddComponent<BaseMonster>();
		int monsIndex = Random.Range (0, monsList.Count - 1);
		
		Debug.Log(mons);
		Debug.Log(monsList);
		Debug.Log(monsIndex);
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


// poison, confuse,parasite
public enum MoveType{
	Physical,
	Special,
	Status
}