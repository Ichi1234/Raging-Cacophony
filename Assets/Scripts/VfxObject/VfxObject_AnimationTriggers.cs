using UnityEngine;

public class VfxObject_AnimationTriggers : MonoBehaviour
{
    private VfxObject currentObject;

    private void Awake()
    {
        currentObject = GetComponentInParent<VfxObject>();
    }

    private void AnimationFinish()
    {
        Destroy(currentObject.gameObject);
    }
}
