using UnityEngine;


public class Entity_Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private UI_HealthBar uiHealthbar;

    public float curHealth { get; protected set; }

    private void Awake()
    {
        curHealth = maxHealth;
    }

    private void Start()
    {
        uiHealthbar.SetupMaxSliderHealthValue(maxHealth);
        UpdateHealthUI();
    }

    protected virtual void Update()
    {
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void UpdateHealthUI()
    {
        uiHealthbar.UpdateHealth(curHealth);
    }

    public float GetPercentHealth() => curHealth / maxHealth * 100;

    public virtual bool TakeDamage(AttackData attackData, float attackDir, Collider2D targetCollision)
    {
        Entity_Vfx entityVfx = GetComponent<Entity_Vfx>();

        ReduceHealth(attackData.damage);
        entityVfx.CreateOnHitEffect();

        return true;
    }

    public void ReduceHealth(float damage)
    {
        curHealth -= damage;
        UpdateHealthUI();
    }
}
