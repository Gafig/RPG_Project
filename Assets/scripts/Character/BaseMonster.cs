using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMonster : Character {

	public BattleManager control;
	
	// Use this for initialization
	void Start () {
		control = BattleManager.instance;
	}
	
	// Update is called once per frame
	public void Update () {
		UpdateAfterBlueprint();
		// isDead();
	}

	void isDead(){
		if(this.getHP() <= 0){
			Destroy(this);
		}
	}

	void OnMouseDown(){
    	// Debug.Log(transform.name);
		this.getHP();
		// Debug.Log("HP " + getHP());
		this.getAttackStat();
		this.getDefenceStat();
		this.getEvasionStat();
		control.clicked(this.transform.gameObject);
	}

}

// public enum Rarity{
// 	Common,
// 	Rare,
// 	VeryRare,
// 	human
// }


