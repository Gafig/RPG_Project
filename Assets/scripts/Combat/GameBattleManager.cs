using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleManager : MonoBehaviour {
	public GameObject battleCamera;

	public GameObject followingCamera;

	public GameObject player;
	public List<BaseMonster> allMonster = new List<BaseMonster>();
	public List<MonsterMoves> allMoves = new List<MonsterMoves> ();

	public Transform defencePodium;
	public Transform attackPodium;
	public GameObject emptyMons;

	// public BattleManager bm;
	 

	// Use this for initialization
	void Start () {
		//playerCamera.SetActive (false);
		followingCamera.SetActive (true);
		battleCamera.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterBattle(Rarity rarity){
		followingCamera.SetActive (false);
		battleCamera.SetActive (true);

		BaseMonster battleMonster = GetRandomMonsterFromList (GetMonsterByRarity (rarity));
		Debug.Log (battleMonster.name);


		GameObject dMons = Instantiate (emptyMons, defencePodium.transform.position, Quaternion.identity) as GameObject; 
		Vector3 monsLocalPos = new Vector3 (0, 1, 0);
		dMons.transform.parent = defencePodium;
		dMons.transform.localPosition = monsLocalPos;

		BaseMonster tempMons = dMons.AddComponent<BaseMonster> () as BaseMonster;
		tempMons.Addmember(battleMonster);

		dMons.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;
	
		// bm.ChangeMenu (BattleMenu.Selection);
	}

	public List<BaseMonster> GetMonsterByRarity(Rarity rarity){
		List<BaseMonster> returnMonster = new List<BaseMonster> ();
		foreach (BaseMonster Monster in allMonster) {
			if (Monster.rarity == rarity)
				returnMonster.Add (Monster);
		}

		return returnMonster;
	} 

	public BaseMonster GetRandomMonsterFromList(List<BaseMonster> monsList){
		BaseMonster mons = new BaseMonster ();
		int monsIndex = Random.Range (0, monsList.Count - 1);
		mons = monsList[monsIndex];
		return mons;
	}
}


[System.Serializable]

public class MonsterMoves{
	string Name;
	public MoveType category;
	public Stat moveStat;
	public MonsterType moveType;
	public int PP;
	public float Power;
	public float accuracy;
}

[System.Serializable]
public class Stat
{
	public float minimum;
	public float maximum;
}

// poison, confuse,parasite
public enum MoveType{
	Physical,
	Special,
	Status
}