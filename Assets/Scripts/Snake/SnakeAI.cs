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
    private Vector2 direction;

    public FieldOfView fov;

    // Start is called before the first frame update
    void Start()
    {
        state = EState.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        if(fov.targetDetected) {
            state = EState.CHASE;	
		}
        else { 
            switch(state) {
                case EState.PATROL:
                case EState.ALERT:
                    state = EState.PATROL;
                    break;
                case EState.CHASE:
                    state = EState.ALERT;
                    break;
		    }	
		}

        switch(state) {
            case EState.CHASE:
				// rotate towards target
				direction = chaseTarget.transform.position - transform.position;
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed *Time.deltaTime);

				// move towards cursor
				transform.position = Vector3.MoveTowards(transform.position, chaseTarget.transform.position, chaseSpeed * Time.deltaTime);
                break;
		}
    }
}
