using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Move
    public Vector2 MoveInput;
    public bool MovePressed;

    // Jump
    public bool JumpPressed;
    public bool IsGrounded = true;

    // Attack
    public bool AttackPressed;
    public int ComboStep = 0;
    public float ComboTimer = 0f;
    public float ComboDuration = 0.5f;

    // Roll
    public bool RollPressed;
    public bool IsRolling = false;
    public bool CanRoll = true;

    // Guard
    public bool GuardPressed;
    public bool IsGuarding = false;
    public bool CanGuard = true;

    //  Facing
    public int FacingDir = 1;   // 1 = right, -1 = left
}