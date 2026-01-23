using System.Collections;
using UnityEngine;

public class Entity_Vfx : MonoBehaviour
{
    [Header("VFX prefab")]
    [SerializeField] private Transform attackVfx;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject bigSmokePrefab;
    [SerializeField] private Material onHitMat;

    private SpriteRenderer sr;
    private Material originalMat;
    private Coroutine onHitEffectCo;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    public void SpawnAttackObject()
    {
        attackVfx.gameObject.SetActive(true);
    }

    public void CreateOnHitEffect()
    {
        if (onHitEffectCo != null)
        {
            StopCoroutine(onHitEffectCo);
        }

        onHitEffectCo = StartCoroutine(CreateOnHitEffectCo());
    }

    private IEnumerator CreateOnHitEffectCo()
    {
        sr.material = onHitMat;

        yield return new WaitForSeconds(0.1f);

        sr.material = originalMat;
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
