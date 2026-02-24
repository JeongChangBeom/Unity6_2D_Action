using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerBlackboard blackboard;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Roll")]
    [SerializeField] private float rollForce = 12f;
    [SerializeField] private float rollDuration = 0.25f;
    private float rollTimer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleJump();
        HandleRoll();
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        if (blackboard.IsRolling)
        {
            return;
        }

        float speed = blackboard.MoveInput.x * moveSpeed;
        if (blackboard.MovePressed)
        {
            speed *= 1.5f;
        }

        float velocityChange = speed - rb.linearVelocity.x;
        rb.AddForce(new Vector2(velocityChange * rb.mass, 0), ForceMode2D.Impulse);
    }

    private void HandleJump()
    {
        if (!blackboard.JumpPressed || !blackboard.IsGrounded)
        {
            return;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        blackboard.IsGrounded = false;
    }

    private void HandleRoll()
    {
        if (blackboard.IsRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0f) blackboard.IsRolling = false;
            return;
        }

        if (!blackboard.RollPressed)
        {
            return;
        }

        blackboard.IsRolling = true;
        rollTimer = rollDuration;

        float inputDir = blackboard.MoveInput.x;
        if (Mathf.Abs(inputDir) < 0.1f)
        {
            inputDir = blackboard.FacingDir;
        }

        rb.AddForce(new Vector2(inputDir * rollForce, 0), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            blackboard.IsGrounded = true;
        }
    }
}