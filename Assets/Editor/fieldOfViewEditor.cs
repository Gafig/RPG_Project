﻿using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class fieldOfViewEditor : Editor {
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.back, Vector3.up, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.directionToAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.directionToAngle(+fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);

        }
    }

}
