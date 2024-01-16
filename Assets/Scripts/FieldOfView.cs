using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    Vector2 Angle2Dir(float angleInDegrees) {
        return new Vector2(Mathf.Sin(angleInDegrees), Mathf.Cos(angleInDegrees)); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
