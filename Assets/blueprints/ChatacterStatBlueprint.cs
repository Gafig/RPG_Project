using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character stat", menuName = "character/stat")]
public class ChatacterStatBlueprint : ScriptableObject {

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
}
