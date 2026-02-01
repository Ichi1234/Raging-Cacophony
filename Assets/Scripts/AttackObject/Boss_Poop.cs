using UnityEngine;

public class Boss_Poop : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50;
    private Rigidbody2D rb;
    private SpriteRenderer[] poopParts;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        poopParts = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        RandomPoopShape();
        rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y);
    }

    private void Update()
    {
        transform.Rotate(0, 0, -1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void RandomPoopShape()
    {
        foreach (var poop in poopParts)
        {
            float randomXShape = Random.Range(0.7f, 1);

            poop.transform.localScale = new Vector3(randomXShape, poop.transform.localScale.y, poop.transform.localScale.z);
        }
    }
}
