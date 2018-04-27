using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConversationTrigger : Event {

    public DialogConversation conversation;
    public DialogConversationBluePrint blueprint;
    private bool started = false;

    public override void trigger()
    {
        FindObjectOfType<DialogManager>().startConversation(conversation);
        FindObjectOfType<DialogManager>().currentEvent = this;
        //Debug.Log("Trigger" + this);
    }

    private void Start()
    {
        if (blueprint != null)
            conversation = new DialogConversation(blueprint.dialogs);
    }

    public void Update()
    {
        if (blueprint != null)
            conversation = new DialogConversation(blueprint.dialogs);
        /*if(FindObjectOfType<DialogManager>().currentEvent == this)
        {
            if (!FindObjectOfType<DialogManager>().isBetweenConversation)
                triggerNextEvent();
        }*/
    }

}
