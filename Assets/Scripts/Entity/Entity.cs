using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Movement Detail")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float dashForce;
    [SerializeField] protected float dashDuration;
    [SerializeField] protected float dashCooldown = 5;

    protected float baseMoveSpeed;

    [Header("Collision Detection")]
    [SerializeField] private float checkGroundLine;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Details")]
    public Entity_Combat entityCombat { get; private set; }
    protected Rigidbody2D rb;
    protected Animator anim;
    public bool canFlip { get; private set; } = true;
    public bool isGround { get; private set; }

    public float facingDir { get; private set; } = 1;

    public StateMachine stateMachine { get; private set; }

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public float DashForce => dashForce;
    public float DashDuration => dashDuration;
    public float DashCooldown => dashCooldown;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        entityCombat = GetComponent<Entity_Combat>();

        baseMoveSpeed = moveSpeed;

        anim.SetBool("isIdle", true);
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        stateMachine.CallUpdateCurrentState();

        isGround = GroundDetected();

        HandleFlip();
    }

    public void AnimationTriggered()
    {
        stateMachine.currentState.AnimationTriggered();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -checkGroundLine));

    }
   

    protected bool GroundDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, checkGroundLine, groundLayer);
    }


    public void SetVelocity(float velocityX, float velocityY)
    {
        rb.linearVelocity = new Vector2(velocityX, velocityY);
    }

    public void SetCanFlip(bool canFlip) => this.canFlip = canFlip;

    public virtual void HandleFlip()
    {

    }


    public virtual void HandleFlip(float moveDir=1)
    {

    }

    public void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }
}
