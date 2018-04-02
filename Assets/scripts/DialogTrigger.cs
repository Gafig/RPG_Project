using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour, Event {

    public Dialog dialog;

    public void trigger()
    {
        FindObjectOfType<DialogManager>().startDialog(dialog);
    }
}
