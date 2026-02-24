using UnityEngine;

public class PlayerBlackboard : MonoBehaviour
{
    public Vector2 MoveInput;

    public bool MovePressed;
    public bool JumpPressed;
    public bool AttackPressed;
    public bool RollPressed;
    public bool GuardPressed;

    public int FacingDir = 1;   // 1 = right, -1 = left

    public int ComboStep = 0;
    public float ComboTimer = 0f;
    public float ComboDuration = 0.5f;

    public bool IsGrounded = true;
    public bool IsRolling = false;
}