using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public string PName;
	public Sprite image;
	public int AttackStat;
	public int DefenceStat;
	public int SpeedStat;
	public int SpAttackStat;
	public int EvasionStat;
	public int maxHP;
	public int maxMana;
	public int maxStamina;
	public Rarity rarity;

	public ChatacterStatBlueprint statBlueprint;
	private ChatacterStatBlueprint previousStatBlueprint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAfterBlueprint();
	}

	public void UpdateAfterBlueprint(){
		if(statBlueprint != null )
			if(previousStatBlueprint != statBlueprint)
			{
				Debug.Log("change");
				this.PName = statBlueprint.name;
				this.image = statBlueprint.image;
				this.AttackStat = statBlueprint.AttackStat;
				this.DefenceStat = statBlueprint.DefenceStat;
				this.SpeedStat = statBlueprint.SpeedStat;
				this.SpAttackStat = statBlueprint.SpAttackStat;
				this.EvasionStat = statBlueprint.EvasionStat;
				this.maxHP = statBlueprint.maxHP;
				this.maxMana = statBlueprint.maxMana;
				this.maxStamina = statBlueprint.maxStamina;
				this.rarity = statBlueprint.rarity;
				previousStatBlueprint = statBlueprint;
				
			}
	}
}

public enum Rarity{
	Common,
	Rare,
	VeryRare,
	human
}

