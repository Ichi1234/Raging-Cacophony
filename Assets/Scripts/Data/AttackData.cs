using UnityEngine;

public struct AttackData
{
    public float damage;
    public float knockback;
    public PlayerAttackTypes? playerAttackType;

    public AttackData(float damage, float knockback, PlayerAttackTypes? playerAttackType = null)
    {
        this.damage = damage;
        this.knockback = knockback;
        this.playerAttackType = playerAttackType;
    }
}