using UnityEngine;

public class Entity_Vfx : MonoBehaviour
{
    [Header("VFX prefab")]
    [SerializeField] private Transform attackVfx;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject bigSmokePrefab;

    private void Awake()
    {
        
    }
    public void CreateAttackVfx()
    {
        attackVfx.gameObject.SetActive(true);
    }

    public GameObject CreateSmokeVfx()
    {
        Vector3 smokeOffset = new Vector3(0.5f * GetComponent<Boss>().facingDir, 0, 0);
        return Instantiate(smokePrefab, transform.position + smokeOffset, smokePrefab.transform.rotation, transform);
    }

    public GameObject CreateBigSmoke()
    {
        return Instantiate(bigSmokePrefab, transform);
    }
}
