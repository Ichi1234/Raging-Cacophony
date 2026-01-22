using UnityEngine;


public class Entity_Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private UI_HealthBar uiHealthbar;

    public float curHealth;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    private void Start()
    {
        uiHealthbar.SetupMaxSliderHealthValue(maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        uiHealthbar.UpdateHealth(curHealth);
    }

    public float GetPercentHealth() => curHealth / maxHealth * 100;

    public void TakeDamage(float damage, float attackDir, float knockback, Collider2D targetCollision)
    {
        ReduceHealth(damage);

        if (targetCollision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = targetCollision.GetComponent<Player>();
            player.KnockBack(attackDir, knockback);
        }

    }

    public void ReduceHealth(float damage)
    {
        curHealth -= damage;
        UpdateHealthUI();
    }
}
