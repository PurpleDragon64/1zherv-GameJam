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

        // if player is not holding any movement keys, dash upwards
        inputDirection = inputDirection == Vector2.zero ? Vector2.up : inputDirection;
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = rotation;

        rb.velocity = inputDirection.normalized.magnitude * dashSpeed * transform.right;

        // animation and sound
        moveLocked = true;
        animator.SetTrigger("Dash");

        SoundManager.Instance.PlayDash();
        yield return new WaitForSeconds(dashDuration);
        moveLocked = false;
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        animator.ResetTrigger("Dash");
        canDash = true;
    }

    public void TriggerDash() { 
	    StartCoroutine(Dash());
    }
}
