using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    private float vfxTimer;
    [SerializeField] private float timeBeforeDestroy = 0.5f;

    private void Start()
    {
        vfxTimer = timeBeforeDestroy;
    }

    private void Update()
    {
        vfxTimer -= Time.deltaTime;
        
        if (vfxTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
