using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckValidMoney : Event {
    [SerializeField]
    Event valid;
    [SerializeField]
    Event invalid;
    public int cost;

    public override void trigger()
    {
        if (Wallet.instance.validToSpend(cost))
            nextEvent = valid;
        else
            nextEvent = invalid;
        triggerNextEvent();
    }
}
