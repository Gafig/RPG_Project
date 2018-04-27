using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRelation : Event {

    public string character;
    public int value;

    public override void trigger()
    {
        FlagController.instance.updateRelation(character, value);
    }
}
