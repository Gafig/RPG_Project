using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveAround : MonoBehaviour
{

    float speed = 5.0f;

    void Update()
    {
        if (GameMasterController.IsInputEnabled)
        {
            var move = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0));

            float speed = this.speed;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, move, 0.26F);

            foreach (RaycastHit hit in hits.OrderBy(x => x.distance))
            {
                speed = 0f;
            }
            transform.position += move * speed * Time.deltaTime;
        }
    }

}