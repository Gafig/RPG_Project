using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConversationTrigger : MonoBehaviour {

    public DialogConversation conversation;

    public void trigger()
    {
        FindObjectOfType<DialogManager>().startConversation(conversation);
    }
}
