using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
    public enum EState {PATROL, CHASE, ALERT};
    public EState state;
    public GameObject chaseTarget;

    public float rotateSpeed = 25;
    public float moveSpeed = 25;
    private Vector2 direction;

    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public float FOVRadius;
    public float FOVAngle;

    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        state = EState.PATROL;
        StartCoroutine(FOVCheck());
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case EState.CHASE:
				// rotate towards cursor
				direction = chaseTarget.transform.position - transform.position;
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed *Time.deltaTime);

				// move towards cursor
				transform.position = Vector3.MoveTowards(transform.position, chaseTarget.transform.position, moveSpeed * Time.deltaTime);
                break;
		}
    }

    private IEnumerator FOVCheck() {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while(true) {
            yield return wait;

            if(TargetDetected()) {
                state = EState.CHASE; 
		    }
            else { 
                if(state == EState.CHASE) {
                    state = EState.ALERT;	
				}
                else {
                    state = EState.PATROL;	
				}
	        }
		}
    }

    private bool TargetDetected() {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, FOVRadius, targetLayer);

        if(rangeCheck.Length > 0) {
            Transform target = rangeCheck[0].transform;
            Vector2 direction2target = (target.position - transform.position).normalized;

            if(Vector2.Angle(transform.up, direction2target) < FOVAngle/2) {
                float distance2target = Vector2.Distance(transform.position, target.position);

                Debug.DrawRay(transform.position, direction2target, Color.red);

                if(!Physics2D.Raycast(transform.position, direction2target, distance2target, obstructionLayer)) {
                    print("detected player");
                    return true;
				}
		    }
		}

        print("not detected");
        return false;
    }
}
