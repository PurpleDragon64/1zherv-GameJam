using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Animator animator;

    private Rigidbody2D rb;

    private Vector2 inputDirection;

    public float dashSpeed = 30f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
    private bool canMove = true;
    private bool canDash = true;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }

        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            print("Dash");
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = rotation;

        rb.velocity = inputDirection.normalized.magnitude * transform.right * moveSpeed;

        // animations
        animator.SetFloat("MoveSpeed", inputDirection.magnitude);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        rb.velocity = inputDirection.normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        canMove = false;

        yield return new WaitForSeconds(dashCooldown);
        canMove = true;
        canDash = true;
    }

    private void OnGameStateChange(GameState state)
    {
        print("From PlayerMovemet: game state changed");
        canMove = state == GameState.Playing;
    }
}
