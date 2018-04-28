using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPanelController : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI money;

	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        money.text = string.Format("{0:F2}", Wallet.instance.balance);
	}
}
