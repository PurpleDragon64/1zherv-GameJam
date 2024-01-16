using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float FOVRadius;

    [Range(0,360)]
    public float FOVAngle;
    public bool targetDetected;

    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    // Returns direction vector corresponding to angle in degrees
    public Vector3 Angle2Dir(float angleInDegrees) {
        // Calculate relative to local rotation of target
        angleInDegrees += transform.eulerAngles.z;
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0); 
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVCheck());   
    }

    private IEnumerator FOVCheck() {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while(true) {
            yield return wait;

            targetDetected = TargetDetected();

		}
    }

    private bool TargetDetected() {
        // store all targets detected in radius
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, FOVRadius, targetLayer);

        // if any targets detected
        if(rangeCheck.Length > 0) {
            // take the first detected target
            Transform target = rangeCheck[0].transform;
            Vector2 direction2target = (target.position - transform.position).normalized;

            // if the target is in the fov cone
            if(Vector2.Angle(transform.right, direction2target) < FOVAngle/2) {
                float distance2target = Vector2.Distance(transform.position, target.position);

                if(Physics2D.Raycast(transform.position, direction2target, distance2target)) {
                    return true;
				}
		    }
		}

        return false;
    }
}
