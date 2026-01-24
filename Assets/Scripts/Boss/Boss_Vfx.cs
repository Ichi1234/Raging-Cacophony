using UnityEngine;

public class Boss_Vfx : Entity_Vfx
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject bigSmokePrefab;

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
