using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Roll")]
    [SerializeField] private float rollForce = 12f;
    [SerializeField] private float rollDuration = 0.25f;
    [SerializeField] private float rollCooldown = 0.4f;

    [Header("Attack")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1.2f, 0.8f);
    [SerializeField] private Vector2 attackOffset = new Vector2(0.8f, 0.4f);

    [Header("Guard")]
    [SerializeField] private Vector2 guardBoxSize = new Vector2(1.4f, 1.0f);
    [SerializeField] private Vector2 guardOffset = new Vector2(0.6f, 0.5f);
    [SerializeField] private float guardDuration = 0.5f;
    [SerializeField] private float guardCooldown = 0.6f;

    private float rollTimer;
    private float rollCooldownTimer;
    private float guardTimer;
    private float guardCooldownTimer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TickTimers();
        Jump();
        Roll();
        Attack();
        Guard();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void TickTimers()
    {
        if (rollCooldownTimer > 0f)
        {
            rollCooldownTimer -= Time.deltaTime;
            if (rollCooldownTimer <= 0f)
            {
                playerState.CanRoll = true;
            }
        }

        if (guardTimer > 0f)
        {
            guardTimer -= Time.deltaTime;
            if (guardTimer <= 0f)
            {
                playerState.IsGuarding = false;
            }
        }

        if (guardCooldownTimer > 0f)
        {
            guardCooldownTimer -= Time.deltaTime;
            if (guardCooldownTimer <= 0f)
            {
                playerState.CanGuard = true;
            }
        }
    }

    private void Move()
    {
        if (playerState.IsRolling)
        {
            return;
        }

        float speed = playerState.MoveInput.x * moveSpeed;
        if (playerState.MovePressed)
        {
            speed *= 1.5f;
        }

        float velocityChange = speed - rb.linearVelocity.x;
        rb.AddForce(new Vector2(velocityChange * rb.mass, 0), ForceMode2D.Impulse);
    }

    private void Jump()
    {
        if (!playerState.JumpPressed || !playerState.IsGrounded)
        {
            return;
        }

        playerState.JumpPressed = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        playerState.IsGrounded = false;
    }

    private void Roll()
    {
        if (playerState.IsRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0f)
            {
                playerState.IsRolling = false;
            }
            return;
        }

        if (!playerState.RollPressed)
        {
            return;
        }

        if (!playerState.CanRoll || !playerState.IsGrounded)
        {
            playerState.RollPressed = false;
            return;
        }

        playerState.RollPressed = false;
        playerState.CanRoll = false;

        playerState.IsRolling = true;
        rollTimer = rollDuration;
        rollCooldownTimer = rollCooldown;

        float inputDir = playerState.MoveInput.x;
        if (Mathf.Abs(inputDir) < 0.1f)
        {
            inputDir = playerState.FacingDir;
        }

        rb.AddForce(new Vector2(inputDir * rollForce, 0), ForceMode2D.Impulse);
    }

    private void Attack()
    {
        if (!playerState.AttackPressed)
        {
            return;
        }

        playerState.AttackPressed = false;

        Vector2 center = (Vector2)transform.position +
                         new Vector2(attackOffset.x * playerState.FacingDir, attackOffset.y);

        DrawDebugBox(center, attackBoxSize, Color.red, 0.1f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, attackBoxSize, 0f, hitMask);
    }

    private void Guard()
    {
        if (playerState.IsGuarding)
        {
            Vector2 activeCenter = (Vector2)transform.position +
                                   new Vector2(guardOffset.x * playerState.FacingDir, guardOffset.y);

            DrawDebugBox(activeCenter, guardBoxSize, Color.blue, 0f);
            Collider2D[] activeHits = Physics2D.OverlapBoxAll(activeCenter, guardBoxSize, 0f, hitMask);
            return;
        }

        if (!playerState.GuardPressed)
        {
            return;
        }

        playerState.GuardPressed = false;

        if (!playerState.CanGuard)
        {
            return;
        }

        playerState.CanGuard = false;
        playerState.IsGuarding = true;
        guardTimer = guardDuration;
        guardCooldownTimer = guardCooldown;
    }

    private void DrawDebugBox(Vector2 center, Vector2 size, Color color, float duration)
    {
        Vector2 half = size * 0.5f;

        Vector2 bl = center + new Vector2(-half.x, -half.y);
        Vector2 br = center + new Vector2(half.x, -half.y);
        Vector2 tr = center + new Vector2(half.x, half.y);
        Vector2 tl = center + new Vector2(-half.x, half.y);

        Debug.DrawLine(bl, br, color, duration);
        Debug.DrawLine(br, tr, color, duration);
        Debug.DrawLine(tr, tl, color, duration);
        Debug.DrawLine(tl, bl, color, duration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            playerState.IsGrounded = true;
        }
    }
}