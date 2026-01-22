using UnityEngine;

public class Entity_Vfx : MonoBehaviour
{
    [Header("VFX prefab")]
    [SerializeField] private Transform attackVfx;
    [SerializeField] private GameObject smokePrefab;

    private void Awake()
    {
        
    }
    public void CreateAttackVfx()
    {
        attackVfx.gameObject.SetActive(true);
    }

    public void CreateSmokeVfx()
    {
        Instantiate(smokePrefab, transform);
    }
}
