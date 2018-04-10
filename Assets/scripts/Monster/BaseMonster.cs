using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMonster : MonoBehaviour {

	public string PName;
	public Sprite image;
	public MonsterType type;
	public Rarity rarity;
	public int HP;
	private int maxHP;
	public Stat AttackStat;
	public Stat DefenceStat;

	public MonsterStats monsterStats;

	private int level;

	// Use this for initialization
	void Start () {
		maxHP = HP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Addmember(BaseMonster bp){
		this.PName = bp.PName;
		this.image = bp.image;
		this.type = bp.type;
		this.rarity = bp.rarity;
		this.HP = bp.HP;
		this.maxHP = bp.maxHP;
		this.AttackStat = bp.AttackStat;
		this.DefenceStat = bp.DefenceStat;
		this.monsterStats = bp.monsterStats;
		this.level = bp.level;
	}
}

public enum Rarity{
	VeryCommon,
	Common,
	SemiRare,
	Rare,
	VeryRare
}


public enum MonsterType{
	Flying,
	Ground,
	Rock,
	Steel,
	Fire,
	Water,
	Grass,
	Ice,
	Electric,
	Phychic,
	Dark,
	Dragon,
	Fighting,
	Normal

}


[System.Serializable]

public class MonsterStats{
	public int AttackStat;
	public int DefenceStat;
	public int SpeedStat;
	public int SpAttackStat;
	public int SpDefenceStat;
	public int EvasionStat;
}