using UnityEngine;

public class Boss_Poop : AttackObject
{
    [SerializeField] private float moveSpeed = 50;
    private Rigidbody2D rb;
    private SpriteRenderer[] poopParts;
    private Boss_Vfx bossVfx;
    private Player player;
    private AttackData projectileAttackData = new AttackData(5, 2);

    public void Initialize(Boss_Vfx vfx, Boss_Combat combat)
    {
        bossVfx = vfx;
        combatInfo = combat;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        poopParts = GetComponentsInChildren<SpriteRenderer>();

        player = FindAnyObjectByType<Player>();

    }

    private void Start()
    {
        RandomPoopShape();

    }

    private void Update()
    {
        transform.Rotate(0, 0,  2);

    }

    private void OnEnable()
    {
        ShootPoop();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            combatInfo.SetAttackData(projectileAttackData);
            combatInfo.PerformAttack(collision);
        }

        gameObject.SetActive(false);

        bossVfx.ReturnPoop(gameObject);

    }

    private float CalculatedAngle()
    {
        Vector3 direction = player.transform.position - transform.position;

        float angleDegree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool playerOnLeft = direction.x < 0;

        float minAngle = playerOnLeft ? 135f : 5f;
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
