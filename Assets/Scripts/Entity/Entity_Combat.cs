using UnityEngine;

public abstract class Entity_Combat : MonoBehaviour
{
    [Header("Attack Details")]
    [SerializeField] protected float attackDamage = 10;
    [SerializeField] protected float attackCooldown = 1;
    public float baseAttackDamage { get; protected set; }
    public float baseAttackCoolDown { get; protected set; }

    public Transform attackPosition;
  
    public Vector3 attackHitboxSize;
    public Vector3 attackHitboxOffset;

    protected bool isHitOpponent = false;

    protected AttackData currentAttackData;

    public float AttackDamage => attackDamage;
    public float AttackCooldown => attackCooldown;

    private void Awake()
    {
        baseAttackDamage = attackDamage;
        baseAttackCoolDown = attackCooldown;
    }

    public void SetAttackDamage(float damage) => attackDamage = damage;
    public void SetAttackCooldown(float cooldown) => attackCooldown = cooldown;

    public void SetAttackData(AttackData data)
    {
        currentAttackData = data;
    }

    public virtual void PerformAttack(Collider2D targetCollision)
    {
        if (IsOpponent(targetCollision))
        {
            Entity_Health entityHealth = targetCollision.gameObject.GetComponent<Entity_Health>();
            float attackDir = transform.position.x < targetCollision.transform.position.x ? 1 : -1;
            isHitOpponent = entityHealth.TakeDamage(currentAttackData, attackDir, targetCollision);
        }
    }


    protected bool IsOpponent(Collider2D targetCollision)
    {
        return targetCollision.gameObject.layer == LayerMask.NameToLayer("Boss") 
            || targetCollision.gameObject.layer == LayerMask.NameToLayer("Player") 
            && targetCollision.gameObject.layer != gameObject.layer;
    }
}
