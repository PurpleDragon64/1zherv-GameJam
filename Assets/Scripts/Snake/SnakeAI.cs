using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAI : MonoBehaviour
{
    // debug
    public enum EState {PATROL, CHASE, ALERT};
    public EState state;

    public GameObject chaseTarget;
    public Transform[] waypoints;
    private int waypointIdx;
    private Transform waypoint;
    private int waypointsCount;


    public float patrolSpeed = 5;
    public float chaseSpeed = 10;
    public float patrolAcceleration = 6;
    public float chaseAcceleration = 13;
    public float patrolRotateSpeed = 5;
    public float chaserotateSpeed = 25;
    private float rotateSpeed = 5;

    public FieldOfView fov;
    [Range(0,360)]
    public float defaultFOVAngle;

    private Vector3 targetPos;

    public NavMeshAgent agent;




    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // used for enabeling and disabeling snake movement
        agent.isStopped = true;

        waypointIdx = 0;
        waypoint = waypoints[waypointIdx];
        waypointsCount = waypoints.Length;
        targetPos = waypoint.position;


        state = EState.PATROL;
        fov.FOVAngle = defaultFOVAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.targetDetected)
        {
            // set state
            state = EState.CHASE;
            // set parameters
            agent.speed = chaseSpeed;
            agent.acceleration = chaseAcceleration;
            rotateSpeed = chaserotateSpeed;

            targetPos = chaseTarget.transform.position;
        }

        // destination reached
        if (agent.remainingDistance < 0.5f)
        {
            if (state == EState.PATROL)
            {
                waypoint = waypoints[++waypointIdx % waypointsCount];
            }
            else
            {
                // set state
                state = EState.PATROL;
                // set parameters
                agent.speed = patrolSpeed;
                agent.acceleration = patrolAcceleration;
                rotateSpeed = patrolRotateSpeed;
            }

            targetPos = waypoint.position;
        }

        RotateSnake();
        MoveSnake();
    }

    void RotateSnake()
    {
        Vector3 velocity = agent.velocity;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    void MoveSnake()
    {
        agent.SetDestination(targetPos);
    }
}
