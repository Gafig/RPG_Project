using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : Event {

    [SerializeField]
    GameObject yesNoUI, ui;
    YesNo yn;
    [SerializeField]
    DialogConversation question;
    public DialogConversationBluePrint blueprint;
    // Use this for initialization
    DialogManager dm;
    [SerializeField]
    Event yes;
    [SerializeField]
    Event no;

    public override void trigger()
    {
        dm.currentEvent = this;
        dm.isQuestion = true;
        dm.startConversation(question);
    }
    void Start () {
        dm = DialogManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (blueprint != null)
            question = new DialogConversation(blueprint.dialogs);
        if (dm.currentEvent == this)
        {
            if (dm.readyToAsk)
            {
                if (yn == null)
                {
                    ui = Instantiate(yesNoUI);
                    yn = ui.GetComponent<YesNo>();
                    setBtn();
                }
            }
        }
    }

    void setBtn()
    {
        removeAllListener();
        yn.yesBtn.onClick.AddListener(() => { work(true); });
        yn.noBtn.onClick.AddListener(() => { work(false); });
    }

    void removeAllListener()
    {
        yn.yesBtn.onClick.RemoveAllListeners();
        yn.noBtn.onClick.RemoveAllListeners();
    }

    void work(bool choice)
    {
        if (choice)
            nextEvent = yes;
        else
            nextEvent = no;
        dm.isBetweenConversation = false;
        dm.endDialog();
        removeAllListener();
        Destroy(ui);
        yn = null;
    }
}
