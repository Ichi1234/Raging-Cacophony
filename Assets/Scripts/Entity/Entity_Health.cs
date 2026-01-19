using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    public float curHealth;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void ReduceHealth(float damage)
    {
        curHealth -= damage;
    }
}
