using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    MoveAround parentData;
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        parentData = transform.parent.GetComponent(typeof(MoveAround)) as MoveAround ;
        StartCoroutine("FindTargetWithDelay", 0.2f);
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, parentData.dir);
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            findvisibleTargets();
        }
    }

    void findvisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        Debug.Log("Find:" + targetInViewRadius.Length);
        foreach( Collider targetCol in targetInViewRadius)
        {
            Transform target = targetCol.transform;
            Vector3 dirToTarget = (target.position - transform.position);
            dirToTarget = dirToTarget.normalized;
            //Debug.Log(transform.rotation.eulerAngles.z);
            //Debug.Log(Vector3.Angle (directionToAngle( transform.rotation.eulerAngles.z, false).normalized, dirToTarget));
            if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
            {
                
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                //Debug.Log(Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask));
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    
                }
            }
        }
    }

    public Vector3 directionToAngle(float degree, bool angleIsGlobal )
    {
        if (!angleIsGlobal)
            degree += transform.eulerAngles.z;
        return new Vector3( Mathf.Sin(-(360-degree) * Mathf.Deg2Rad), Mathf.Cos((360 - degree) * Mathf.Deg2Rad), 0);
    }
}
