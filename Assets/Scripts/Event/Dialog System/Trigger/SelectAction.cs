using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : Event {

    [SerializeField]
    GameObject SelectUI, ui;
    SelectChoices select;
    [SerializeField]
    DialogConversation question;
    public DialogConversationBluePrint blueprint;
    // Use this for initialization
    DialogManager dm;
    [SerializeField]
    Event c1;
    [SerializeField]
    Event c2;
    [SerializeField]
    Event c3;
    public string[] choices = new string[3]; 

    public override void trigger()
    {
        dm.currentEvent = this;
        dm.isQuestion = true;
        dm.startConversation(question);
    }
    void Start()
    {
        dm = DialogManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (blueprint != null)
            question = new DialogConversation(blueprint.dialogs);
        if (dm.currentEvent == this)
        {
            if (dm.readyToAsk)
            {
                if (select == null)
                {
                    ui = Instantiate(SelectUI);
                    select = ui.GetComponent<SelectChoices>();
                    setBtn();
                }
            }
        }
    }

    void setBtn()
    {
        removeAllListener();
        select.btn1.onClick.AddListener(() => { work(1); });
        select.btn2.onClick.AddListener(() => { work(2); });
        select.btn3.onClick.AddListener(() => { work(3); });
        select.c1.text = choices[0];
        select.c2.text = choices[1];
        select.c3.text = choices[2];
    }

    void removeAllListener()
    {
        select.btn1.onClick.RemoveAllListeners();
        select.btn2.onClick.RemoveAllListeners();
        select.btn3.onClick.RemoveAllListeners();
    }

    void work(int choice)
    {
        if (choice == 1)
            nextEvent = c1;
        else if (choice == 2)
            nextEvent = c2;
        else
            nextEvent = c3;
        dm.isBetweenConversation = false;
        dm.endDialog();
        removeAllListener();
        Destroy(ui);
        select = null;
    }
}
