using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour
{

    float speed = 10.0f;

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
    }
}