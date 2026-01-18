using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }

    private Rigidbody2D rb;

    public Vector2 moveInput { get; private set; }
    public PlayerInputSet input { get; private set; }


    [Header("Movement Detail")]
    public float moveSpeed = 4;
    public float jumpForce = 12;
    private Coroutine jumpAnimationCo;


    [Header("Collision Detection")]
    [SerializeField] private float checkGroundLine;
    [SerializeField] private LayerMask groundLayer;
    public bool isGround { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();
        rb = GetComponent<Rigidbody2D>();

        idleState = new Player_IdleState(this, stateMachine, "isIdle");
        moveState = new Player_MoveState(this, stateMachine, "isMoving");
        jumpState = new Player_JumpState(this, stateMachine, "");

        stateMachine.Initialize(idleState);


    }

    protected override void Update()
    {
        base.Update();

        isGround = GroundDetected();
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -checkGroundLine));
    }

    private bool GroundDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, checkGroundLine, groundLayer);
    }

    public void JumpAnimation()
    {
        if (jumpAnimationCo != null)
        {
            StopCoroutine(jumpAnimationCo);
        }

        jumpAnimationCo = StartCoroutine(JumpAnimationCo(transform.localScale));
    }

    public IEnumerator JumpAnimationCo(Vector3 originalScale)
    {
        Vector3 shrinkTo = new Vector3(originalScale.x / 2, originalScale.y * 1.5f);

        StartCoroutine(PlayerTransformAnimation(transform.localScale, shrinkTo, 0.5f));

        yield return new WaitForSeconds(1);

        StartCoroutine(PlayerTransformAnimation(transform.localScale, originalScale, 0.5f));

    }

    public IEnumerator PlayerTransformAnimation(Vector3 start, Vector3 end, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.localScale = end;
    }

}
