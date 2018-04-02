using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConversationTrigger : MonoBehaviour {

    public DialogConversation conversation;

    public void Trigger()
    {
        FindObjectOfType<DialogManager>().startConversation(conversation);
    }
}
