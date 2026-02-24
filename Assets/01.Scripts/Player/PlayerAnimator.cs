using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerBlackboard blackboard;
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

        anim.SetBool("IsGrounded", blackboard.IsGrounded);
        anim.SetBool("IsRolling", blackboard.IsRolling);
    }

    private void UpdateMove()
    {
        float speed = Mathf.Abs(blackboard.MoveInput.x);
        speed = speed > 0.01f ? (blackboard.MovePressed ? 1.5f : 1f) : 0f;
        anim.SetFloat("Speed", speed);
    }

    private void UpdateFlip()
    {
        if (blackboard.MoveInput.x > 0.01f)
        {
            transform.localScale = Vector3.one;
            blackboard.FacingDir = 1;
        }
        else if (blackboard.MoveInput.x < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            blackboard.FacingDir = -1;
        }
    }

    private void UpdateActions()
    {
        if (blackboard.JumpPressed)
        {
            anim.SetTrigger("Jump");
            blackboard.JumpPressed = false;
        }

        if (blackboard.RollPressed)
        {
            anim.SetTrigger("Roll");
            blackboard.RollPressed = false;
        }

        if (blackboard.AttackPressed)
        {
            if (blackboard.ComboStep < 3)
            {
                blackboard.ComboStep++;
                switch (blackboard.ComboStep)
                {
                    case 1: anim.SetTrigger("Attack1"); break;
                    case 2: anim.SetTrigger("Attack2"); break;
                    case 3: anim.SetTrigger("Attack3"); break;
                }
                blackboard.ComboTimer = blackboard.ComboDuration;
            }
            blackboard.AttackPressed = false;
        }

        if (blackboard.GuardPressed)
        {
            anim.SetTrigger("Guard");
            blackboard.GuardPressed = false;
        }
    }

    private void UpdateComboTimer()
    {
        if (blackboard.ComboStep > 0)
        {
            blackboard.ComboTimer -= Time.deltaTime;
            if (blackboard.ComboTimer <= 0f) blackboard.ComboStep = 0;
        }
    }
}