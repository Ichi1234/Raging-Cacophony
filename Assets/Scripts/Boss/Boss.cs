using UnityEngine;
using UnityEngine.Windows;

public class Boss : Entity
{
    [SerializeField] private Player player;

    [Header("Collision Detection")]
    [SerializeField] private float checkWallLine;
    [SerializeField] private LayerMask wallLayer;
    public bool wallDetected { get; private set; }

    public Boss_IdleState idleState { get; private set; }
    public Boss_MoveState moveState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isIdle", true);

        idleState = new Boss_IdleState(this, stateMachine, "isIdle");
        moveState = new Boss_MoveState(this, stateMachine, "isMoving");
       

        stateMachine.Initialize(idleState);

    }

    protected override void Update()
    {
        base.Update();

        wallDetected = WallDetected();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public override void HandleFlip(float moveDir = 1)
    {
        if (moveDir != facingDir && moveDir != 0 && canFlip)
        {
            Flip();
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(checkWallLine * -facingDir, 0));
    }


    protected bool WallDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * -facingDir, checkWallLine, wallLayer);
    }


}
