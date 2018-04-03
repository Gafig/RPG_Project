using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    Vector2 velocity;
    [SerializeField]
    float smoothTimeX;
    [SerializeField]
    float smoothTimeY;
    [SerializeField]
    bool isBound;
    Transform player;
    [SerializeField]
    Vector3 minPos, maxPos;
    [SerializeField]
    bool fallowPlayer = true;
	void Start () {
        player = GameObject.Find("Player").transform;
	}

    private void FixedUpdate()
    {
        if (fallowPlayer)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);

            if (isBound)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                    Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                    Mathf.Clamp(transform.position.z, minPos.z, maxPos.z));
            }
        }
    }
}
