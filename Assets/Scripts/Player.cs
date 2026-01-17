using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    private Vector2 moveInput;
    private PlayerInputSet input;

    private void Awake()
    {
        input = new PlayerInputSet();
    }

    public override void Update()
    {
        base.Update();

        Debug.Log(moveInput);
    }


    private void OnEnable()
    {
        input.Enable();

        input.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Move.canceled += context => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
