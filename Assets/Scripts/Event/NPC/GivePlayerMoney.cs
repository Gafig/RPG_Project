using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerMoney : Event {

    public int cost;
    public override void trigger()
    {
        Wallet.instance.add(cost);
        triggerNextEvent();
    }
}
