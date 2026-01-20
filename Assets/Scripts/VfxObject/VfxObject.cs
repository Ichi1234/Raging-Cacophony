using UnityEngine;

public class VfxObject : MonoBehaviour
{
    protected Entity_Combat combatInfo;

    private void Awake()
    {
        combatInfo = GetComponentInParent<Entity_Combat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            combatInfo.PerformAttack(collision);
        }
        

        
    }

}
