using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Event {

    [SerializeField]
    Animator anim;
    [SerializeField]
    bool open = false;
    private void Start()
    {
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    public override void trigger()
    {
        open = !open;
        GameMasterController.instance.startEvent();
        StartCoroutine(Animate());
    }

    public IEnumerator Animate()
    {
        anim.SetBool("open", open);
        yield return new WaitForSeconds(1f);
        //Debug.Log("Open the door");
        GameMasterController.instance.endEvent();
    }
}
