using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour, Event {

    public Dialog dialog;
    public DialogBluePrint blueprint;

    private void Start()
    {
        if (blueprint != null)
            dialog = new Dialog(blueprint.name, blueprint.sentences);
    }

    public void Update()
    {
        if(blueprint!=null)
            dialog = new Dialog(blueprint.name, blueprint.sentences);
    }

    public void trigger()
    {
        FindObjectOfType<DialogManager>().startDialog(dialog);
    }
}
