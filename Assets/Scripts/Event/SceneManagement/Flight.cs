﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : Event {

	public GameObject battleCamera;
	public GameObject followingCamera;

	public override void trigger()
    {
        Debug.Log("Start combat event");
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
		
    }

    private IEnumerator fadeIn()
    {
        Fade.instance.fadeIn();
        yield return new WaitUntil(() => !Fade.instance.isFading);
    }
}
