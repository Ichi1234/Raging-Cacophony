using UnityEngine;

public class Entity_Vfx : MonoBehaviour
{
    [Header("Attack Details")]
    [SerializeField] private VfxObject attackPrefab;
    [SerializeField] private Transform attackPosition;

    private void Awake()
    {
        
    }
    public void CreateAttackVfx()
    {
        Instantiate(attackPrefab, attackPosition);
    }
}
