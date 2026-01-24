using UnityEngine;

public class AttackObject_AnimationTriggers : MonoBehaviour
{
    private AttackObject currentObject;
    private Entity_AnimationTriggers entityAnimationTriggers;

    private void Awake()
    {
        currentObject = GetComponentInParent<AttackObject>();
        entityAnimationTriggers = transform.root.GetComponentInChildren<Entity_AnimationTriggers>();
    }

    private void AnimationFinish()
    {
        currentObject.gameObject.SetActive(false);
        entityAnimationTriggers.AnimationFinish();
    }
}
