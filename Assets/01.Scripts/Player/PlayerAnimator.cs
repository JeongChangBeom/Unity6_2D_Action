using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateMove();
        UpdateFlip();
        UpdateActions();
        UpdateComboTimer();

        UpdateStateParams();
    }

    private void UpdateStateParams()
    {
        anim.SetBool("IsGrounded", playerState.IsGrounded);
        anim.SetBool("IsRolling", playerState.IsRolling);
    }

    private void UpdateMove()
    {
        float speed = Mathf.Abs(playerState.MoveInput.x);
        speed = speed > 0.01f ? (playerState.MovePressed ? 1.5f : 1f) : 0f;
        anim.SetFloat("Speed", speed);
    }

    private void UpdateFlip()
    {
        if (playerState.MoveInput.x > 0.01f)
        {
            transform.localScale = Vector3.one;
            playerState.FacingDir = 1;
        }
        else if (playerState.MoveInput.x < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            playerState.FacingDir = -1;
        }
    }

    private void UpdateActions()
    {
        UpdateJump();
        UpdateRoll();
        UpdateAttack();
        UpdateGuard();
    }

    private void UpdateJump()
    {
        if (!playerState.JumpPressed)
        {
            return;
        }

        anim.SetTrigger("Jump");
    }

    private void UpdateRoll()
    {
        if (!(playerState.RollPressed && playerState.CanRoll))
        {
            return;
        }

        anim.SetTrigger("Roll");
    }

    private void UpdateAttack()
    {
        if (!playerState.AttackPressed)
        {
            return;
        }

        if (playerState.ComboStep < 3)
        {
            playerState.ComboStep++;

            switch (playerState.ComboStep)
            {
                case 1:
                    anim.SetTrigger("Attack1");
                    break;
                case 2:
                    anim.SetTrigger("Attack2");
                    break;
                case 3:
                    anim.SetTrigger("Attack3");
                    break;
            }

            playerState.ComboTimer = playerState.ComboDuration;
        }
    }

    private void UpdateGuard()
    {
        if (!(playerState.GuardPressed && playerState.CanGuard))
        {
            return;
        }

        anim.SetTrigger("Guard");
    }

    private void UpdateComboTimer()
    {
        if (playerState.ComboStep > 0)
        {
            playerState.ComboTimer -= Time.deltaTime;
            if (playerState.ComboTimer <= 0f) playerState.ComboStep = 0;
        }
    }
}