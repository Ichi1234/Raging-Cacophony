using UnityEngine;

public class Player_Health : Entity_Health
{
    public float regenPerSecond { get; private set; } = 0;

    public override bool TakeDamage(AttackData attackData, float attackDir, Collider2D targetCollision)
    {
        Entity_Vfx entityVfx = GetComponent<Entity_Vfx>();

        ReduceHealth(attackData.damage);
        entityVfx.CreateOnHitEffect();

        if (targetCollision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = targetCollision.GetComponent<Player>();
            player.KnockBack(attackDir, attackData.knockback);
        }

        return true;
    }


    protected override void Update()
    {
        base.Update();

        RegenerateHealth();
    }

    private void RegenerateHealth()
    {
        if (curHealth < maxHealth)
        {
            curHealth += regenPerSecond * Time.deltaTime;
            curHealth = Mathf.Min(curHealth, maxHealth);
            UpdateHealthUI();
        }
    }

    public void SetRegenerateHealthPerSecond(float regenPerSec) => regenPerSecond = regenPerSec;
}
