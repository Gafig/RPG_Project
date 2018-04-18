using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantInteract : MonoBehaviour, Interactable{

    public EventHandler interaction;

    public void react()
    {
        interaction = gameObject.GetComponent<EventHandler>();
        if (interaction == null)
        {
            Debug.Log("Error");
            return;
        }
        interaction.triggerEvents();
    }

    // Use this for initialization
    void Start () {
        interaction = gameObject.GetComponent<EventHandler>();
        if (interaction == null)
        {
            Debug.Log("Error");
            return;
        }
        else
            StartCoroutine(delay());
	}

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        react();
    }
}
