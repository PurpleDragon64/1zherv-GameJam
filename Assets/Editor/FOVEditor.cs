using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        // draw a circle with radius of fov.radius
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.up, 360, fov.FOVRadius);

        // calculate angles of the fov cone
        Vector3 viewAngleA = fov.Angle2Dir(-fov.FOVAngle / 2);
        Vector3 viewAngleB = fov.Angle2Dir(fov.FOVAngle / 2);

        // draw lines of the fov cone
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.FOVRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.FOVRadius);
        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + fov.FOVRadius * fov.transform.right);
    }
}
