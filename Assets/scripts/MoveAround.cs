using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveAround : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed = 5.0f;
    Vector3 velocity;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameMasterController.IsInputEnabled)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * speed;
            transform.LookAt(velocity);
           
           
        }
    }

    private void FixedUpdate()
    {
        float speed = this.speed;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        RaycastHit[] hitsX = null, hitsY = null;
        if(velocity.x != 0)
            hitsX = Physics.RaycastAll(transform.position, new Vector3(velocity.x, 0, 0), 0.26F);
        if(velocity.y != 0)
            hitsY = Physics.RaycastAll(transform.position, new Vector3(0, velocity.y, 0), 0.26F);
        if (hitsX == null || hitsX.Length == 0)
            rigidbody.MovePosition(rigidbody.position + new Vector3(velocity.x, 0, 0) * Time.fixedDeltaTime);
        if (hitsY == null || hitsY.Length == 0)
            rigidbody.MovePosition(rigidbody.position + new Vector3(0, velocity.y, 0) * Time.fixedDeltaTime);
    }
}