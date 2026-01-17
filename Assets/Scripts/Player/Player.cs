using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState playerMoveState { get; private set; }

    public Vector2 moveInput { get; private set; }
    private PlayerInputSet input;
    public float moveSpeed = 4;


    protected override void Awake()
    {
        base.Awake();

        idleState = new Player_IdleState(this, stateMachine, "isIdle");
        playerMoveState = new Player_MoveState(this, stateMachine, "isMoving");



        input = new PlayerInputSet();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
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
