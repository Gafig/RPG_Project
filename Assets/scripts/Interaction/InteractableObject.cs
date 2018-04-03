using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, Interactable {

    public new string name;
    public Event interaction;
    public Transform interactTranform;
    public bool hasInteracted;
    public bool isFocus;
    public GameObject player;
    public PlayerInteraction playerInteraction;
    [Range(0.5f, 1.5f)]
    public float radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void react()
    {
        interaction = gameObject.GetComponent<Event>();
        if(interaction == null)
        {
            Debug.Log("Error");
            return;
        }
        interaction.trigger();
    }

    void Start ()
    {
        hasInteracted = false;
        isFocus = false;
        player = GameObject.Find("Player");
        if(player != null)
        {
            playerInteraction = player.GetComponent<PlayerInteraction>();
        }
	}

    private void FixedUpdate()
    {
        if (isFocus)
        {
            if (playerInteraction.focusObject != this)
                defocus();
            if (Input.GetKeyDown("z"))
                if (!hasInteracted)
                {
                    hasInteracted = true;
                    react();
                }
        }
        checkInteract();
    }

    private void checkInteract()
    {
        if (!GameMasterController.instance.betweenEvent)
            hasInteracted = false;
    }

    public void focus()
    {
        isFocus = true;
    }

    public void defocus()
    {
        isFocus = false;
    }
}
