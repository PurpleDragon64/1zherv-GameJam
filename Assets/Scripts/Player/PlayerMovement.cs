using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    public float moveSpeed = 10f;
    public float dashSpeed = 30f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
    private bool canDash = true;

    public bool moveLocked = false; // for enabling end disabling player movement

    private Vector2 inputDirection; // store inforamtion about input

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLocked)
        {
            return;
        }

        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            TriggerDash();
        }
    }

    private void FixedUpdate()
    {
        if (moveLocked)
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

        rb.velocity = inputDirection.normalized.magnitude * moveSpeed * transform.right;

        // animations
        animator.SetFloat("MoveSpeed", inputDirection.magnitude);
    }

    private IEnumerator Dash()
    {
        moveLocked = true;
        rb.velocity = inputDirection.normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        moveLocked = false;
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void TriggerDash() { 
	    StartCoroutine(Dash());
    }
}
