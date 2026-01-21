using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Movement Detail")]
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    public float dashDuration;
    public float dashCooldown = 5;


    [Header("Collision Detection")]
    [SerializeField] private float checkGroundLine;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Details")]
    [SerializeField] public Entity_Combat entityCombat;

    protected Rigidbody2D rb;
    protected Animator anim;
    public bool canFlip = true;
    public bool isGround { get; private set; }

    public float facingDir = 1;

    public StateMachine stateMachine { get; private set; }

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        entityCombat = GetComponent<Entity_Combat>();

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

    public virtual void HandleFlip()
    {

    }


    public virtual void HandleFlip(float moveDir=1)
    {

    }

    protected void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }
}
