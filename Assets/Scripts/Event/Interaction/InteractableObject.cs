using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, Interactable {

    public new string name;
    public EventHandler interaction;
    //public Transform interactTranform;
    public bool hasInteracted;
    public bool isFocus;
    public GameObject player;
    public PlayerInteraction playerInteraction;

    [SerializeField]
    bool needPress = true;

    [Range(0.5f, 1.5f)]
    public float radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void react()
    {
        interaction = gameObject.GetComponent<EventHandler>();
        if(interaction == null)
        {
            Debug.Log("Error");
            return;
        }
        interaction.triggerEvents();
    }

    void Start ()
    {
        interaction = gameObject.GetComponent<EventHandler>();
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
        if(playerInteraction != null)
        {
            if (playerInteraction.focusObject == this.gameObject)
            {
                focus();
            }
        }
        if (isFocus)
        {
            if (playerInteraction.focusObject != this.gameObject)
                defocus();
            if (Input.GetKeyDown("z") || !needPress)
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
