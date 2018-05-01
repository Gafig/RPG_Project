using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : Event {

	public GameObject battleCamera;
	public GameObject followingCamera;

	public override void trigger()
    {
        
		followingCamera.SetActive(false);
        battleCamera.SetActive(true);
        
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
		
    }

    private IEnumerator fadeIn()
    {
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
    }
}
