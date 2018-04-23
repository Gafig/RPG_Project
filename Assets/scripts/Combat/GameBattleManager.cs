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
	public GameObject emptyMons1;
	public GameObject emptyMons2;
	public GameObject characterWilliam;
	public GameObject characterPhilip;
	public GameObject characterJane;
	public GameObject characterRose;
	public GameObject dMons;
	public GameObject william;
	public GameObject philip;
	public GameObject jane;
	public GameObject rose;
	
	public Sprite sprite;

	public static GameBattleManager instance;

	public List<GameObject> dMonsList =  new List<GameObject>();
	public List<GameObject> charList=  new List<GameObject>();
	 
	private void Awake()
    {
        if (instance == null){
			Debug.Log("awake");
            instance = this;
		}
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		followingCamera.SetActive (true);
		battleCamera.SetActive (false);

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerBattle(){
		william = Instantiate (characterWilliam, attackPodium.transform.position, Quaternion.identity) as GameObject; 
		philip = Instantiate (characterPhilip, attackPodium.transform.position, Quaternion.identity) as GameObject; 
		jane = Instantiate (characterJane, attackPodium.transform.position, Quaternion.identity) as GameObject; 
		rose = Instantiate (characterRose, attackPodium.transform.position, Quaternion.identity) as GameObject; 

		charList.Add(william);
		charList.Add(philip);
		charList.Add(jane);
		charList.Add(rose);

		// Debug.Log("COUNT CHAR" + charList.Count);
		Debug.Log("CHARSLIST" + charList[0] + charList[1]+ charList[2] + charList[3]);

		Vector3 characterLocalPos = new Vector3 (0, 1, 0);
		william.transform.parent = attackPodium;
		william.transform.localPosition = characterLocalPos;
		william.transform.localScale = new Vector3(8,29,29);
		// character.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;
		philip.transform.parent = attackPodium;
		philip.transform.localPosition = characterLocalPos;
		philip.transform.localScale = new Vector3(8,29,29);
		// character1.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;
		jane.transform.parent = attackPodium;
		jane.transform.localPosition = characterLocalPos;
		jane.transform.localScale = new Vector3(8,29,29);
		// character2.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;
		rose.transform.parent = attackPodium;
		rose.transform.localPosition = characterLocalPos;
		rose.transform.localScale = new Vector3(8,29,29);
		// character3.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;
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
		// Debug.Log("DMONSLIST" + dMonsList);

		GameMasterController.instance.setPermanantUI(false);
        // Debug.Log("setPermanantUI(false)");

		// Debug.Log(GetMonsterByRarity(rarity));

		BaseMonster battleMonster = GetRandomMonsterFromList(GetMonsterByRarity(rarity));
		
		Vector3 monsLocalPos = new Vector3 (0, 1, 0);
		dMons.transform.parent = defencePodium;
		dMons.transform.localPosition = monsLocalPos;
		dMons.transform.localScale = new Vector3(8,29,29);
		dMons.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;

		
		// character.GetComponent<SpriteRenderer> ().sprite = battleMonster.image;

		// BaseMonster tempMons = dMons.AddComponent<BaseMonster> () as BaseMonster;
		// tempMons.Addmember(battleMonster);
		// Debug.Log(battleMonster);
		// tempMons = battleMonster;

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