using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{

    private float prob;

    public GameObject battleCamera;
    public GameObject followingCamera;

    public Party party;

    [Header("Selection")]
    public GameObject selectionMenu;


    [Header("Misc")]
    public int currentSelection;
    public Button atkBtn;
    public Button spBtn;
    public Button runBtn;

    public static BattleManager instance;

    public bool isBattleEnd = false;

    List<Character> objListOrder = new List<Character>();

    List<Character> characterList = new List<Character>();
    List<BaseMonster> monsterList = new List<BaseMonster>();
    List<BaseMonster> monsterSelect;

    float sumHp = 0;
    float sumMaxHp = 0;
    float flee = 0;
    public GameObject clickedGameObject;
    public BaseMonster monsterSelected;
    public int attackStat;
    public Flight flight;

    [Header("select")]
    public GameObject select1;
    public GameObject select2;
    public GameObject select3;
    public GameObject playerselect1;
    public GameObject playerselect2;
    public GameObject playerselect3;
    public GameObject playerselect4;

    [Header("text massage")]
    public TextMeshProUGUI msg;

    [Header("monster info")]
    public TextMeshProUGUI monsterName1;
    public TextMeshProUGUI monsterName2;
    public TextMeshProUGUI monsterName3;
    public Slider monsterHealth1;
    public Slider monsterHealth2;
    public Slider monsterHealth3;

    [Header("character info")]
    public GameObject characterPanel1;
    public GameObject characterPanel2;
    public GameObject characterPanel3;
    public GameObject characterPanel4;
    public TextMeshProUGUI characterName1;
    public TextMeshProUGUI characterName2;
    public TextMeshProUGUI characterName3;
    public TextMeshProUGUI characterName4;
    public TextMeshProUGUI characterHP1;
    public TextMeshProUGUI characterHP2;
    public TextMeshProUGUI characterHP3;
    public TextMeshProUGUI characterHP4;
    public Slider playerHealth1;
    public Slider playerHealth2;
    public Slider playerHealth3;
    public Slider playerHealth4;

    
    public int show;
    public List<Character> tempCharacter;
    public bool isStart = true;
    public int characterUIIndex;
    public int tempCountList;
    public bool isWilliamAlive = true;
    public string tempText = "";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start()
    {

        atkBtn = GameObject.Find("Atk").GetComponent<Button>();
        spBtn = GameObject.Find("Sp").GetComponent<Button>();
        runBtn = GameObject.Find("Run").GetComponent<Button>();
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);

        playerselect1.SetActive(true);
        playerselect2.SetActive(false);
        playerselect3.SetActive(false);
        playerselect4.SetActive(false);
        currentSelection = 0;

    }

    void OnEnable()
    {
        isBattleEnd = false;
        msg.text = "Monsters appear";
        atkBtn.interactable = true;
        spBtn.interactable = true;
        runBtn.interactable = true;


    }

    // Update is called once per frame
    void Update()
    {

        // perform something
        if (objListOrder.Count == 0)
        {
            Debug.Log("enter queue");
            queue();
        }

        if (monsterSelected == null && !isBattleEnd)
        {
            monsterSelected = monsterList[0];
            select1.SetActive(false);
            select2.SetActive(false);
            select3.SetActive(false);

            monsterSelected.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }



        if (isStart)
        {
            monsterName1.text = GameBattleManager.instance.dMonsList[0].GetComponent<BaseMonster>().getPName();
            monsterName2.text = GameBattleManager.instance.dMonsList[1].GetComponent<BaseMonster>().getPName();
            monsterName3.text = GameBattleManager.instance.dMonsList[2].GetComponent<BaseMonster>().getPName();
            monsterHealth1.maxValue = GameBattleManager.instance.dMonsList[0].GetComponent<BaseMonster>().getMaxHP();
            monsterHealth1.value = monsterHealth1.maxValue;

            monsterHealth2.maxValue = GameBattleManager.instance.dMonsList[1].GetComponent<BaseMonster>().getMaxHP();
            monsterHealth2.value = monsterHealth2.maxValue;

            monsterHealth3.maxValue = GameBattleManager.instance.dMonsList[2].GetComponent<BaseMonster>().getMaxHP();
            monsterHealth3.value = monsterHealth3.maxValue;

            tempCharacter = new List<Character>(characterList);

            show = tempCharacter.Count - 1;
            tempCountList = tempCharacter.Count - 1;

            if (show >= 0)
            {
                characterName1.text = tempCharacter[0].getPName();
                characterPanel1.SetActive(true);
                playerHealth1.maxValue = characterList[0].getMaxHP();
                playerHealth1.value = characterList[0].getHP();
                characterHP1.text = playerHealth1.value + " / " + playerHealth1.maxValue;

            }
            if (show >= 1)
            {
                characterName2.text = tempCharacter[1].getPName();
                characterPanel2.SetActive(true);
                playerHealth2.maxValue = characterList[1].getMaxHP();
                playerHealth2.value = characterList[1].getHP();
                characterHP2.text = playerHealth2.value + " / " + playerHealth2.maxValue;

            }
            if (show >= 2)
            {
                characterName3.text = tempCharacter[2].getPName();
                characterPanel3.SetActive(true);
                playerHealth3.maxValue = characterList[2].getMaxHP();
                playerHealth3.value = characterList[2].getHP();
                characterHP3.text = playerHealth3.value + " / " + playerHealth3.maxValue;

            }
            if (show >= 3)
            {
                characterName4.text = tempCharacter[3].getPName();
                characterPanel4.SetActive(true);
                playerHealth4.maxValue = characterList[3].getMaxHP();
                playerHealth4.value = characterList[3].getHP();
                characterHP4.text = playerHealth4.value + " / " + playerHealth4.maxValue;

            }

            isStart = false;
        }


    }

    public void queue()
    {

        for (int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++)
        {

            objListOrder.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
            monsterList.Add(GameBattleManager.instance.dMonsList[i].GetComponent<BaseMonster>());
        }

        monsterSelect = new List<BaseMonster>(monsterList);

        for (int i = 0; i < GameBattleManager.instance.charList.Count; i++)
        {
            if (GameBattleManager.instance.charList[i].GetComponent<Character>().isAlive())
            {
                objListOrder.Add(GameBattleManager.instance.charList[i].GetComponent<Character>());
                characterList.Add(GameBattleManager.instance.charList[i].GetComponent<Character>());

            }
        }

        if (objListOrder.Count != 0)
        {
            objListOrder.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
            objListOrder.Reverse();
            if (characterList.Count != 0 && monsterList.Count != 0)
            {
                characterQueue();
                monsterQueue();
            }

        }
        currentSelection = 0;
        characterUIIndex = 0;


    }

    public void characterQueue()
    {
        characterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        characterList.Reverse();
        for (int i = 0; i < characterList.Count; i++)
        {
            sumMaxHp += characterList[i].getMaxHP();
        }
    }

    public void monsterQueue()
    {
        monsterList.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
        monsterList.Reverse();

    }

    public void clicked(GameObject click)
    {
        clickedGameObject = click;
        Debug.Log("asdafwf" + clickedGameObject.GetComponent<BaseMonster>().getHP());
        monsterSelected = clickedGameObject.GetComponent<BaseMonster>();
        select1.SetActive(false);
        select2.SetActive(false);
        select3.SetActive(false);
        monsterSelected.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("parent child " + monsterSelected.transform.parent.gameObject.transform.GetChild(0));

        msg.text = characterList[currentSelection].getPName() + " attacking " + monsterSelected.getPName();
    }


    public void attackFunction()
    {
        if (currentSelection < characterList.Count)
        {

            if (!isBattleEnd)
            {
                attackStat = characterList[currentSelection].getAttackStat();

                //player atk monster selected
                monsterSelected.attacked(attackStat);
            }
            attackHelp();

        }

    }

    public void spAttackFunction()
    {
        if (currentSelection < characterList.Count)
        {
            Debug.Log("sp");

            if (!isBattleEnd)
            {
                attackStat = characterList[currentSelection].getSpAttackStat();
            }

            attackHelp();
            //use mana
            characterList[currentSelection].useMana(20);


        }

    }


    private void attackHelp()
    {
        tempText = "";
        if (currentSelection > characterList.Count - 1)
        {
            currentSelection = 0;
        }
        tempText += "now " + characterList[currentSelection].getPName() + " attacking " + monsterSelected.getPName();

        //update monster healthbar
        setHealthBar();

        if (monsterSelected.getHP() <= 0)
        {
            monsterList.Remove(monsterSelected);
            Destroy(monsterSelected.gameObject);
            if (monsterList.Count == 0)
            {
                endCombat();
                addMoney();
            }

        }
        else
        {

            //monster atk player if monster not die
            characterList[currentSelection].attacked(monsterSelected.getAttackStat());
            tempText += ", then " + monsterSelected.getPName() + " attack " + characterList[currentSelection].getPName();

        }
        //destroy player that die

        if (currentSelection < characterList.Count && characterList[currentSelection].getHP() <= 0)
        {
            if (characterList[currentSelection].getPName() == "william")
            {
                Debug.Log("W die");
                msg.text = "william die";
                isWilliamAlive = false;
                endCombat();
            }
            else
            {
                if (characterList.Count == 4)
                    playerHealth4.transform.parent.gameObject.SetActive(false);
                if (characterList.Count == 3)
                    playerHealth3.transform.parent.gameObject.SetActive(false);
                if (characterList.Count == 2)
                    playerHealth2.transform.parent.gameObject.SetActive(false);
                if (characterList.Count == 1)
                    playerHealth1.transform.parent.gameObject.SetActive(false);

                characterList[currentSelection].gameObject.SetActive(false);
                characterList.Remove(characterList[currentSelection]);
                updateCharacterHealthBar();

                currentSelection--;
                // updateCharacterSelect();
                if (characterList.Count == 0)
                {
                    endCombat();
                    currentSelection = 0;
                }

            }
        }
        //die all end battle
        if (!isBattleEnd)
        {
            updateCharacterHealthBar();

            currentSelection++;
            characterUIIndex++;

            if (characterUIIndex > tempCountList)
            {
                characterUIIndex = 0;
            }
            if (currentSelection > characterList.Count - 1)
            {
                currentSelection = 0;
            }

            updateCharacterHealthBar();
            msg.text = tempText;
            updateCharacterSelect();
        }
    }

    public void updateCharacterHealthBar()
    {
        if (characterList.Count >= 1)
        {
            characterName1.text = characterList[0].getPName();
            playerHealth1.maxValue = characterList[0].getMaxHP();
            playerHealth1.value = characterList[0].getHP();
            playerHealth1.gameObject.SetActive(true);
            if (playerHealth1.value < 0)
            {
                playerHealth1.value = 0;
            }
            characterHP1.text = playerHealth1.value + " / " + playerHealth1.maxValue;
        }
        if (characterList.Count >= 2)
        {
            characterName2.text = characterList[1].getPName();
            playerHealth2.maxValue = characterList[1].getMaxHP();
            playerHealth2.value = characterList[1].getHP();
            playerHealth2.gameObject.SetActive(true);
            if (playerHealth2.value < 0)
            {
                playerHealth2.value = 0;
            }
            characterHP2.text = playerHealth2.value + " / " + playerHealth2.maxValue;
        }
        if (characterList.Count >= 3)
        {
            characterName3.text = characterList[2].getPName();
            playerHealth3.maxValue = characterList[2].getMaxHP();
            playerHealth3.value = characterList[2].getHP();
            playerHealth3.gameObject.SetActive(true);
            if (playerHealth3.value < 0)
            {
                playerHealth3.value = 0;
            }
            characterHP3.text = playerHealth3.value + " / " + playerHealth3.maxValue;
        }
        if (characterList.Count >= 4)
        {
            characterName4.text = characterList[3].getPName();
            playerHealth4.maxValue = characterList[3].getMaxHP();
            playerHealth4.value = characterList[3].getHP();
            if (playerHealth4.value < 0)
            {
                playerHealth4.value = 0;
            }
            characterHP4.text = playerHealth4.value + " / " + playerHealth4.maxValue;
            playerHealth4.gameObject.SetActive(true);
        }
    }

    public void updateCharacterSelect()
    {
        if (currentSelection == 0)
        {
            playerselect1.SetActive(true);
            playerselect2.SetActive(false);
            playerselect3.SetActive(false);
            playerselect4.SetActive(false);
        }
        if (currentSelection == 1)
        {
            playerselect1.SetActive(false);
            playerselect2.SetActive(true);
            playerselect3.SetActive(false);
            playerselect4.SetActive(false);
        }
        if (currentSelection == 2)
        {
            playerselect1.SetActive(false);
            playerselect2.SetActive(false);
            playerselect3.SetActive(true);
            playerselect4.SetActive(false);
        }
        if (currentSelection == 3)
        {
            playerselect1.SetActive(false);
            playerselect2.SetActive(false);
            playerselect3.SetActive(false);
            playerselect4.SetActive(true);
        }

    }

    public void setHealthBar()
    {
        if (monsterHealth1 == monsterSelected.transform.parent.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Slider>())
        {
            if (monsterSelected.getHP() <= 0)
            {
                monsterHealth1.gameObject.SetActive(false);
            }
            monsterHealth1.value = monsterSelected.getHP();
        }
        if (monsterHealth2 == monsterSelected.transform.parent.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Slider>())
        {
            if (monsterSelected.getHP() <= 0)
            {
                monsterHealth2.gameObject.SetActive(false);
            }
            monsterHealth2.value = monsterSelected.getHP();
        }
        if (monsterHealth3 == monsterSelected.transform.parent.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Slider>())
        {
            if (monsterSelected.getHP() <= 0)
            {
                monsterHealth3.gameObject.SetActive(false);
            }
            monsterHealth3.value = monsterSelected.getHP();
        }
    }

    public void runFunction()
    {

        for (int i = 0; i < characterList.Count; i++)
        {
            sumHp += characterList[i].getHP();
        }
        flee = ((sumHp / sumMaxHp) * 100) - 20;

        float fleeP = Random.Range(0.0f, 100f);

        if (flee < 60 && flee >= 30)
        {
            flee = ((sumHp / sumMaxHp) * 100);
        }
        if (flee < 30 && flee > 0)
        {
            flee = ((sumHp / sumMaxHp) * 100) + 27;
        }
        if (flee <= 0)
        {
            flee = 25;
        }
       
        if (flee > fleeP)
        {
            endCombat();
            msg.text = "Escape";
        }
        else
        {
            Debug.Log("Can't fleeee");
            msg.text = "Can't escape";
        }
        sumHp = 0;
    }


    public void endCombat()
    {
        isBattleEnd = true;
        tempText = "";
        msg.text = "Battle end";
        atkBtn.interactable = false;
        spBtn.interactable = false;
        runBtn.interactable = false;

        StartCoroutine(waitEndBattle());
        
    }

    IEnumerator waitEndBattle()
    {
        yield return new WaitForSeconds(2);
        flight.triggerNextEvent();
        GameMasterController.instance.IsInputEnabled = true;
        followingCamera.SetActive(true);
        battleCamera.SetActive(false);
        GameMasterController.instance.setPermanantUI(true);
        objListOrder.Clear();
        for (int i = 0; i < GameBattleManager.instance.dMonsList.Count; i++)
        {
            Destroy(GameBattleManager.instance.dMonsList[i]);
        }
        GameBattleManager.instance.dMonsList.Clear();

        monsterList.Clear();
        characterList.Clear();
        isStart = true;

        monsterHealth1.gameObject.SetActive(true);
        monsterHealth2.gameObject.SetActive(true);
        monsterHealth3.gameObject.SetActive(true);
        characterPanel1.SetActive(false);
        characterPanel2.SetActive(false);
        characterPanel3.SetActive(false);
        characterPanel4.SetActive(false);
        playerselect1.SetActive(true);
        playerselect2.SetActive(false);
        playerselect3.SetActive(false);
        playerselect4.SetActive(false);

        sumMaxHp = 0;
    }

    public void addMoney()
    {
        Debug.Log("MONEY");
    }

    public void reset()
    {
        sumHp = 0;
        flee = 0;
        sumMaxHp = 0;
    }

}
