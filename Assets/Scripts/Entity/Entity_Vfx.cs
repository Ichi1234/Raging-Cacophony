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

    public Color attackObjectColor { get; private set; } = Color.white;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    public void ChangeVfxColor(Color newColor) => attackObjectColor = newColor;
    public void SpawnAttackObject()
    {
        attackVfx.gameObject.SetActive(true);
        attackVfx.GetComponentInChildren<SpriteRenderer>().color = attackObjectColor;
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
