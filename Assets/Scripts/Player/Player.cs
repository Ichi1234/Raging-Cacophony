using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_AttackState attackState { get; private set; }

    private Rigidbody2D rb;

    public Vector2 moveInput { get; private set; }
    public PlayerInputSet input { get; private set; }


    [Header("Movement Detail")]
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    public float dashDuration;
    public float dashCooldown = 5;

    private Coroutine movementAnimationCo;
    private Vector3 originalScale;


    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();
        rb = GetComponent<Rigidbody2D>();

        idleState = new Player_IdleState(this, stateMachine, "isIdle");
        moveState = new Player_MoveState(this, stateMachine, "isMoving");
        attackState = new Player_AttackState(this, stateMachine, "isAttacking");
        jumpState = new Player_JumpState(this, stateMachine, "");
        dashState = new Player_DashState(this, stateMachine, "");

        stateMachine.Initialize(idleState);

        originalScale = transform.localScale;


    }

    protected override void Update()
    {
        base.Update();

        HandleFlip();
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

    public void SetVelocity(float velocityX, float velocityY)
    {
        rb.linearVelocity = new Vector2(velocityX, velocityY);
    }

    public void HandleFlip()
    {
        if (moveInput.x != facingDir && moveInput.x != 0)
        {
            Flip();
        }
    }

    public void JumpAnimation()
    {
        if (movementAnimationCo != null)
        {
            StopCoroutine(movementAnimationCo);
        }

        movementAnimationCo = StartCoroutine(JumpAnimationCo());
    }

    public void DashAnimation()
    {
        if (movementAnimationCo != null)
        {
            StopCoroutine(movementAnimationCo);
        }

        movementAnimationCo = StartCoroutine(DashAnimationCo());
    }

    public IEnumerator JumpAnimationCo()
    {
        Vector3 shrinkTo = new Vector3(originalScale.x / 2, originalScale.y * 1.2f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, shrinkTo, 0.5f));

        yield return new WaitForSeconds(1);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, originalScale, 0.5f));

    }

    public IEnumerator DashAnimationCo()
    {
        Vector3 shrinkTo = new Vector3(originalScale.x * 1.5f, originalScale.y / 2f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, shrinkTo, 0.5f));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, originalScale, 0.5f));

    }



}
