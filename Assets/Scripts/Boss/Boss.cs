using UnityEngine;
using UnityEngine.Windows;

public class Boss : Entity
{
    [SerializeField] private Player player;

    [Header("Collision Detection")]
    [SerializeField] private float checkBackWallLine;
    [SerializeField] private float checkFrontWallLine;
    [SerializeField] private LayerMask wallLayer;
    public bool backWallDetected { get; private set; }
    public bool frontWallDetected { get; private set; }

    public Boss_IdleState idleState { get; private set; }
    public Boss_MoveState moveState { get; private set; }
    public Boss_BasicAttackState basicAttackState { get; private set; }
    public Boss_LeapAttackState leapAttackState { get; private set; }
    public Boss_SlamAttackState slamAttackState { get; private set; }
    public Boss_PrepareToAttackState prepareToAttackState { get; private set; }
    public Boss_LungeAttackState prepareLungeAttackState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isIdle", true);

        idleState = new Boss_IdleState(this, stateMachine, "isIdle");
        moveState = new Boss_MoveState(this, stateMachine, "isMoving");
        
        basicAttackState = new Boss_BasicAttackState(this, stateMachine, "isBasicAttack");
        prepareToAttackState = new Boss_PrepareToAttackState(this, stateMachine, "isPrepareAttack");
        leapAttackState = new Boss_LeapAttackState(this, stateMachine, "isMoving");
        slamAttackState = new Boss_SlamAttackState(this, stateMachine, "isMoving");
        prepareLungeAttackState = new Boss_LungeAttackState(this, stateMachine, "isMoving", FindAnyObjectByType<Arena>());




        stateMachine.Initialize(idleState);

    }

    protected override void Update()
    {
        base.Update();

        frontWallDetected = FrontWallDetected();
        backWallDetected = BackWallDetected();
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
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(checkBackWallLine * -facingDir, 0));

        Gizmos.color = Color.purple;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(checkFrontWallLine * facingDir, 0));

    }

    protected bool FrontWallDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * facingDir, checkFrontWallLine, wallLayer);
    }

    protected bool BackWallDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * -facingDir, checkBackWallLine, wallLayer);
    }


}
