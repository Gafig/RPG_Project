using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMovement : MonoBehaviour {

    [SerializeField]
    GameObject player;
    public float dir = 180;

    [SerializeField]
    Sprite[] sprites = new Sprite[4];

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameMasterController.instance.player;
        }
    }

    public void facePlayer()
    {
        Vector3 direction = player.transform.position - transform.root.position;
        dir = Vector3.Angle(direction, transform.up);
        if (player.transform.position.x < transform.position.x)
            dir = 360 - dir;
        setSprite();
    }

    public void setDirection(int direction)
    {
        dir = direction;
        setSprite();
    }

    private void setSprite()
    {
        int index = 0;
        if (isValidFacing(0))
            index = 0;
        else if (isValidFacing(90))
            index = 1;
        else if (isValidFacing(180))
            index = 2;
        else if (isValidFacing(270))
            index = 3;
        setSprite(index);
    }

    private bool isValidFacing(float angle)
    {
        const float ANGLE = 45;
        if (angle == 0)
            return dir > 315 || dir < 45f;
        return dir >= (angle - ANGLE) && dir < (angle + ANGLE);
    }

    private void setSprite(int index)
    {
        transform.root.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
