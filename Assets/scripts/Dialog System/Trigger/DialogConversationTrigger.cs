using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConversationTrigger : MonoBehaviour, Event {

    public DialogConversation conversation;
    public DialogConversationBluePrint blueprint;

    public void trigger()
    {
        FindObjectOfType<DialogManager>().startConversation(conversation);
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
    }

}
