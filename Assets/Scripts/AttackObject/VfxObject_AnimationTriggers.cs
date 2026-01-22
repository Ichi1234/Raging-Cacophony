using UnityEngine;

public class VfxObject_AnimationTriggers : MonoBehaviour
{
    private VfxObject currentObject;
    private Entity_AnimationTriggers entityAnimationTriggers;

    private void Awake()
    {
        currentObject = GetComponentInParent<VfxObject>();
        entityAnimationTriggers = transform.root.GetComponentInChildren<Entity_AnimationTriggers>();
    }

    private void AnimationFinish()
    {
        currentObject.gameObject.SetActive(false);
        entityAnimationTriggers.AnimationFinish();
    }
}
