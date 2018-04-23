using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string PName;
    public Sprite image;
    public int AttackStat;
    public int DefenceStat;
    public int SpeedStat;
    public int SpAttackStat;
    public int EvasionStat;
    public int maxHP;
    public int Hp;
    public int maxMana;
    public int maxStamina;
    public Rarity rarity;

    public ChatacterStatBlueprint statBlueprint;
    private ChatacterStatBlueprint previousStatBlueprint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAfterBlueprint();
    }

    public void UpdateAfterBlueprint()
    {
        if (statBlueprint != null)
            if (previousStatBlueprint != statBlueprint)
            {
                // Debug.Log("change");
                this.PName = statBlueprint.name;
                this.image = statBlueprint.image;
                this.AttackStat = statBlueprint.AttackStat;
                this.DefenceStat = statBlueprint.DefenceStat;
                this.SpeedStat = statBlueprint.SpeedStat;
                this.SpAttackStat = statBlueprint.SpAttackStat;
                this.EvasionStat = statBlueprint.EvasionStat;
                this.maxHP = statBlueprint.maxHP;
                this.Hp = maxHP;
                this.maxMana = statBlueprint.maxMana;
                this.maxStamina = statBlueprint.maxStamina;
                this.rarity = statBlueprint.rarity;
                previousStatBlueprint = statBlueprint;

            }
    }

    public string getPName(){
        return this.PName;
    }

    public Sprite getImage(){
        return this.image;
    }

    public void attacked(int atk){
        // Debug.Log("MAX HP" + this.maxHP);
        // Debug.Log("atk  " + atk);
        
        this.Hp -= atk;
        // Debug.Log("MAX HP" + this.maxHP);
    }

    public void spAttack(int atk, int maxMana){
        attacked(atk);
        this.maxMana -= maxMana;
    }

    public int getAttackStat(){
        return this.AttackStat;
    }

    public int getDefenceStat(){
        return this.DefenceStat;
    }

    public int getSpeedStat(){
        return this.SpeedStat;
    }
    public int getSpAttackStat(){
        return this.SpAttackStat;
    }

    public int getEvasionStat(){
        return this.EvasionStat;
    }

    public int getMaxHP(){
        return this.maxHP;
    }

    public int getHP(){
        return this.Hp;
    }

    public int getMaxMana(){
        return this.maxMana;
    }

    public int getMaxStamina(){
        return this.maxStamina;
    }



}

public enum Rarity
{
    Common,
    Rare,
    VeryRare,
    human
}

