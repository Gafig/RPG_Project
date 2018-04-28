using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveAround : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed = 3.0f;
    Vector3 velocity;
    public Vector3 lastVelocity;
    public float dir;
    [SerializeField]
    Sprite[] spriteList;
    [SerializeField]
    RuntimeAnimatorController[] animList;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public static MoveAround instance; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = 180;
        rb = GetComponent<Rigidbody>();
        lastVelocity = Vector3.down;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (GameMasterController.instance.IsInputEnabled)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * speed;
            move();
            setDirection();
            setSprite();

            if (velocity != Vector3.zero)
            {
                anim.SetBool("walk", true);
                //Debug.Log("SetBool: true");
            }
            else
                anim.SetBool("walk", false);
        }
        else
        {
            rb.velocity = Vector3.zero;
            setSprite();
            anim.SetBool("walk", false);
        }
    }

    public void face(Direction dir)
    {
        if (dir == Direction.down)
            faceDown();
        if (dir == Direction.up)
            faceUp();
        if (dir == Direction.left)
            faceLeft();
        if (dir == Direction.right)
            faceRight();
    }

    public void faceDown()
    {
        dir = 180;
        lastVelocity = new Vector3(0, -1, 0).normalized;
    }

    public void faceUp()
    {
        dir = 0;
        lastVelocity = new Vector3(0, 1, 0).normalized;
    }

    public void faceLeft()
    {
        dir = 270;
        lastVelocity = new Vector3(-1, 0, 0).normalized;
    }

    public void faceRight()
    {
        dir = 90;
        lastVelocity = new Vector3(1, 0, 0).normalized;
    }

    private void move()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        RaycastHit[] hitsX = null, hitsY = null;
        Transform pos = gameObject.GetComponent<SphereCollider>().transform;
        if (velocity.x != 0)
            hitsX = Physics.RaycastAll(pos.position, new Vector3(velocity.x, 0, 0), 0.51F);
        if (velocity.y != 0)
            hitsY = Physics.RaycastAll(pos.position, new Vector3(0, velocity.y, 0), 0.51F);
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
            lastVelocity = velocity.normalized;
        }
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
        if(angle == 0)
            return dir > 315 || dir < 45f;
        return dir >= (angle - ANGLE) && dir < (angle + ANGLE);
    }

    private void setSprite(int index)
    {
        if (anim.runtimeAnimatorController == animList[index])
            return;
        anim.runtimeAnimatorController = animList[index];
        //Debug.Log("Change Anim");
    }

}