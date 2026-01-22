using UnityEngine;

public class Boss_ContactDamageCollision : MonoBehaviour
{
    protected Entity_Combat combatInfo;

    private void Awake()
    {
        combatInfo = GetComponentInParent<Entity_Combat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("yes someone enter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            combatInfo.PerformAttack(collision, combatInfo.contactKnockback);
        }
    }
}
