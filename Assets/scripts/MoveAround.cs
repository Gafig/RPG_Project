using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveAround : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;
    Vector3 velocity;
    public float dir;
    [SerializeField]
    Sprite[] spriteList;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = 180;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameMasterController.IsInputEnabled)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * speed;
            move();
            setDirection();
            setSprite();
        }
    }

    private void move()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        RaycastHit[] hitsX = null, hitsY = null;
        if (velocity.x != 0)
            hitsX = Physics.RaycastAll(transform.position, new Vector3(velocity.x, 0, 0), 0.51F);
        if (velocity.y != 0)
            hitsY = Physics.RaycastAll(transform.position, new Vector3(0, velocity.y, 0), 0.51F);
        if (hitsX == null || hitsX.Length == 0)
            rb.MovePosition(rb.position + new Vector3(velocity.x, 0, 0) * Time.fixedDeltaTime);
        if (hitsY == null || hitsY.Length == 0)
            rb.MovePosition(rb.position + new Vector3(0, velocity.y, 0) * Time.fixedDeltaTime);
    }

    private void setDirection()
    {
        if (velocity != Vector3.zero)
        {
            dir = Vector3.Angle(velocity, transform.up);
            if (velocity.x < 0)
            {
                dir = 360 - dir;
            }
        }
    }

    private void setSprite()
    {
        int index = 0;
        if (isValidFacing(0))
            index = 0;
        else if (isValidFacing(45))
            index = 1;
        else if (isValidFacing(90))
            index = 2;
        else if (isValidFacing(135))
            index = 3;
        else if (isValidFacing(180))
            index = 4;
        else if (isValidFacing(225))
            index = 5;
        else if (isValidFacing(270))
            index = 6;
        else
            index = 7;
        setSprite(index);
    }

    private bool isValidFacing(float angle)
    {
        const float ANGLE = 22.5f;
        if(angle == 0)
            return dir > 315 || dir < 22.5f;
        return dir >= (angle - ANGLE) && dir < (angle + ANGLE);
    }

    private void setSprite(int index)
    {
        spriteRenderer.sprite = spriteList[index];
    }

    private void FixedUpdate()
    {
        
    }
}