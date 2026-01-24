using System.Collections;
using UnityEngine;

public class Entity_Vfx : MonoBehaviour
{
    [Header("VFX prefab")]
    [SerializeField] private Transform attackVfx;
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

}
