using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public List<OwnedMonster> ownedMonster = new List<OwnedMonster> ();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class OwnedMonster{
	public string NickName;
	public BaseMonster monster;
	public int level;
	public List<MonsterMoves> moves = new List<MonsterMoves>(); 
}
