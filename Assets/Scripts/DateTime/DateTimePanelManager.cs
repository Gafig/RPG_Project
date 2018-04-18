using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateTimePanelManager : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI date;
    [SerializeField]
    TextMeshProUGUI time;

	// Update is called once per frame
	void Update () {
        string datetext = string.Format("Day: {0:d2}", DateTimeController.instance.getDate());
        date.SetText( datetext );
        time.SetText(DateTimeController.instance.getTime());
        gameObject.SetActive(GameMasterController.instance.showTime);
	}
}
