using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
    public enum EState {PATROL, CHASE, ALERT};
    public EState state;
    public GameObject chaseTarget;

    public float rotateSpeed = 25;
    public float patrolSpeed = 5;
    public float chaseSpeed = 10;

    public FieldOfView fov;
    [Range(0,360)]
    public float defaultFOVAngle;

    private Vector3 targetPos;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        state = EState.PATROL;
        fov.FOVAngle = defaultFOVAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(fov.targetDetected) {
			targetPos = chaseTarget.transform.position;
            state = EState.CHASE;	
		}

        float angle;
        Quaternion rotation;

        switch(state) {
            case EState.CHASE:
                if(transform.position != targetPos) { 
					// rotate towards target
					direction = targetPos - transform.position;
					angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
					rotation = Quaternion.AngleAxis(angle, Vector3.forward);

					transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed *Time.deltaTime);

					// move towards target
				    transform.position = Vector3.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
				}
                else {
                    state = EState.ALERT;	
				}
                break;
            case EState.ALERT:
                    fov.FOVAngle = 360;
                    
                break;	
		}
    }
}
