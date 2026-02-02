using UnityEngine;

public class Boss_Poop : AttackObject
{
    [SerializeField] private float moveSpeed = 50;
    private Rigidbody2D rb;
    private SpriteRenderer[] poopParts;
    private Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        poopParts = GetComponentsInChildren<SpriteRenderer>();
        player = FindAnyObjectByType<Player>();

    }

    private void Start()
    {
        RandomPoopShape();
        ShootPoop();
    }

    private void Update()
    {
        transform.Rotate(0, 0, GetPlayerDirection() * 1.25f);

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Destroy(gameObject);

    }

    private float CalculatedAngle()
    {
        Vector3 direction = player.transform.position - transform.position;

        float angleDegree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool playerOnLeft = direction.x < 0;

        float minAngle = playerOnLeft ? 135f : 0;
        float maxAngle = playerOnLeft ? 175f : 45f;

        float clampedAngle = Mathf.Clamp(angleDegree, minAngle, maxAngle);

        float rad = clampedAngle * Mathf.Deg2Rad;

        return rad;
    }

    private Vector2 CalculatedShootVector()
    {
        float rad = CalculatedAngle();

        Vector2 finalDirection = new Vector2(Mathf.Cos(rad) , Mathf.Sin(rad));

        return moveSpeed * finalDirection;
    }

    [ContextMenu("Shoot Poop")]
    public void ShootPoop()
    {
        rb.linearVelocity = CalculatedShootVector();
    }

    private void RandomPoopShape()
    {
        foreach (var poop in poopParts)
        {
            float randomXShape = Random.Range(0.7f, 1);

            poop.transform.localScale = new Vector3(randomXShape, poop.transform.localScale.y, poop.transform.localScale.z);
           
        }
    }

    protected float GetPlayerDirection() => transform.root.position.x < player.transform.position.x ? 1 : -1;

}
