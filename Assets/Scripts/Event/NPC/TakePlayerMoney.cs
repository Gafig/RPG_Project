using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePlayerMoney : Event {

    public int cost;
    public override void trigger()
    {
        Wallet.instance.spend(cost);
        triggerNextEvent();
    }
}
