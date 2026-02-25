using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerState bb;
    public PlayerInputActions input;

    void Awake()
    {
        input = new PlayerInputActions();
    }

    void OnEnable()
    {
        input.Enable();

        input.Player.Attack.performed += ctx => bb.AttackPressed = true;
        input.Player.Roll.performed += ctx => bb.RollPressed = true;
        input.Player.Jump.performed += ctx => bb.JumpPressed = true;
        input.Player.Block.performed += ctx => bb.GuardPressed = true;

        input.Player.Move.started += ctx => bb.MovePressed = true;
        input.Player.Move.canceled += ctx => bb.MovePressed = false;
    }

    void Update()
    {
        bb.MoveInput = input.Player.Move.ReadValue<Vector2>();
    }
}