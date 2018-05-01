using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleManager : MonoBehaviour
{
    public GameObject battleCamera;
    public GameObject followingCamera;
    public Party party;
    public Transform defencePodiumtemp;
    public Transform defencePodium;
    public Transform defencePodium1;
    public Transform defencePodium2;
    public GameObject emptyMons;
    public GameObject emptyMons1;
    public GameObject emptyMons2;
    public GameObject dMons;
    public GameObject william;
    public GameObject philip;
    public GameObject jane;
    public GameObject rose;
    public Character isWilliamAlive;
    public static GameBattleManager instance;
    public List<GameObject> dMonsList = new List<GameObject>();
    public List<GameObject> charList = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("awake");
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        followingCamera.SetActive(true);
        battleCamera.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerBattle()
    {

        philip = Party.instance.phillip;
        william = Party.instance.william;
        jane = Party.instance.jane;
        rose = Party.instance.rose;
        
        if (william.GetComponent<Character>().isAlive())
        {
            charList.Add(william);

        }
		if (philip.GetComponent<Character>().isAlive())
        {
            charList.Add(philip);
        }
        if (jane.GetComponent<Character>().isAlive())
        {
            charList.Add(jane);
        }
        if (rose.GetComponent<Character>().isAlive())
        {
            charList.Add(rose);
        }

        for (int i = 0; i < GameBattleManager.instance.charList.Count; i++)
        {
            party = Party.instance;
            party.addMember(GameBattleManager.instance.charList[i].GetComponent<Character>());
            if(GameBattleManager.instance.charList[i].GetComponent<Character>().getPName() == "william"){
                isWilliamAlive = GameBattleManager.instance.charList[i].GetComponent<Character>();
            }

            // Debug.Log(GameBattleManager.instance.charList[i].GetComponent<Character>());
        }

        
    }

    public void EnterBattle(Rarity rarity, int position)
    {

        // BattleManager.instance.isBattleEnd = false;

        if (position == 0)
        {
            defencePodiumtemp = defencePodium;
        }
        else if (position == 1)
        {
            defencePodiumtemp = defencePodium1;
        }
        else if (position == 2)
        {
            defencePodiumtemp = defencePodium2;
        }


        if (rarity == Rarity.Common)
        {
            dMons = Instantiate(emptyMons, defencePodiumtemp.transform.position, Quaternion.identity) as GameObject;
        }
        if (rarity == Rarity.Rare)
        {
            dMons = Instantiate(emptyMons1, defencePodiumtemp.transform.position, Quaternion.identity) as GameObject;
        }
        if (rarity == Rarity.VeryRare)
        {
            dMons = Instantiate(emptyMons2, defencePodiumtemp.transform.position, Quaternion.identity) as GameObject;
        }

        dMonsList.Add(dMons);

        GameMasterController.instance.setPermanantUI(false);

        Vector3 monsLocalPos = new Vector3(0, 1, 0);
        dMons.transform.parent = defencePodiumtemp;
        dMons.transform.localPosition = monsLocalPos;
        dMons.transform.localScale = new Vector3(8, 29, 29);
        

    }

}
