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
    private bool isDashing = false;
    private bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
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
        if (isDashing)
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
        //rb.AddForce(inputDirection * 1000, ForceMode2D.Impulse);
        //Vector3 target = transform.position + dashLenght * new Vector3(inputDirection.x, inputDirection.y, transform.position.z);
        //transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * Time.deltaTime);
        isDashing = true;
        canDash = false;
        rb.velocity = inputDirection.normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
