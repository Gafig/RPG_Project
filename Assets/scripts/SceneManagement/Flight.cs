using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : Event {

	public GameObject battleCamera;
	public GameObject followingCamera;
	private int count = 0;

	public BattleManager bm;

	public override void trigger()
    {
        GameMasterController.instance.startEvent();
		followingCamera.SetActive(false);
        battleCamera.SetActive(true);
		Debug.Log("Start combat event and change camera to battle");
        StartCoroutine(fadeOut());
        
    }
	 private IEnumerator fadeOut()
    {
        Fade.instance.fadeOut();
		while(Fade.instance.isFading){
        	yield return null;
        	load();
		}
    }

	 private void load()
    {

		//after combat
        StartCoroutine(fadeIn());
		// Debug.Log(count);
        
        
    }

    private IEnumerator fadeIn()
    {
		Debug.Log("End combat event");
        // GameMasterController.instance.endEvent();
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
    }
}
