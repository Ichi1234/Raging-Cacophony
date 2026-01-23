using UnityEngine;

public class Boss_ContactDamageCollision : MonoBehaviour
{
    protected Boss_Combat combatInfo;

    private void Awake()
    {
        combatInfo = GetComponentInParent<Boss_Combat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            combatInfo.ContactAttack(collision, new AttackData(combatInfo.attackDamage, combatInfo.contactKnockback));
        }
    }
}
