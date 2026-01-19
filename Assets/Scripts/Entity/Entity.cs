using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Detection")]
    [SerializeField] private float checkGroundLine;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Details")]
    [SerializeField] public Entity_Combat entityCombat;
    

    public bool canFlip = true;
    public bool isGround { get; private set; }

    public float facingDir = 1;

    public StateMachine stateMachine { get; private set; }

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();

        entityCombat = GetComponent<Entity_Combat>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        stateMachine.CallUpdateCurrentState();

        isGround = GroundDetected();

    }

    public void AnimationTriggered()
    {
        stateMachine.currentState.AnimationTriggered();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -checkGroundLine));

    }
   

    protected bool GroundDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, checkGroundLine, groundLayer);
    }

    protected IEnumerator ChangeTransformAnimation(Vector3 start, Vector3 end, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.localScale = end;
    }

    protected void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }
}
