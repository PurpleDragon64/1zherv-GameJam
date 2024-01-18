using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAI : MonoBehaviour
{

    public GameObject chaseTarget;
    public Transform[] waypoints;
    public FieldOfView fov;

    private int waypointIdx;
    private int waypointsCount;
    private Transform waypoint; // currently targeted waypoint


    // parameters
    [SerializeField] float patrolSpeed = 5;
    [SerializeField] float chaseSpeed = 10;
    [SerializeField] float patrolAcceleration = 6;
    [SerializeField] float chaseAcceleration = 13;
    [SerializeField] float patrolRotateSpeed = 5;
    [SerializeField] float chaserotateSpeed = 25;
    private float rotateSpeed = 5;
    [Range(0,360)] [SerializeField] float defaultFOVAngle;


    public NavMeshAgent agent; // for navigation
    private Vector3 targetPos; // position towards which the snake is headed


    private enum EState {PATROL, // follow route defined by waypoints
                        CHASE,  // chase after player, when lost sight, go to
                                // last seen position
    };
    private EState state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // because of NavMeshPlus (2d)
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // used for enabeling and disabeling snake movement
        agent.isStopped = true;

        // initialize route
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
        // player in sight
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
                // set next waypoint from route as current
                waypoint = waypoints[++waypointIdx % waypointsCount];
            }
            else // reached position where player was last seen -> return to patrol state
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
        // rotate snake head based on velocity direction
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
