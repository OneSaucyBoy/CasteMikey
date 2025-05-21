using UnityEngine;
using System.Collections;

public class cavemanLogic : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Transform player;
    private bool isActive = false;
    public LayerMask Walls;
    public Vector2 direction;
    private Rigidbody2D rb;
    private bool isStunned = false;
    public float lives = 4f;
    string tag1;
    public float stunTime = 3f;
    private bool isAlive;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tag = gameObject.tag;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("activao");
            isActive = true;
            Vector2 dirAPlayer = (player.position - transform.position).normalized;
            direction = dirAPlayer;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("desactivao");
            isActive = false;
        }
    }
    void Update()
    {
        if (lives <= 0)
        {
            isAlive = false;
        }
        if (isStunned)
        {
            StartCoroutine(StunCoroutine());
        }
    }
    void FixedUpdate()
    {
        if (isAlive || !isStunned)
        {
            float speed = isActive ? runSpeed : walkSpeed;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.1f, Walls);

            if (hit.collider != null)
            {
                direction = direction == Vector2.left ? Vector2.right : Vector2.left;
                Flip(direction.x);
            }
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else
        {
            gameObject.tag = "Agarrable";
        }
    }
    //paque se voltee
    void Flip(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Latigo"))
        {
            isStunned = true;
            //cambiar tag para que sea aventable 
            gameObject.tag = "Agarrable";
        }
    }
    IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(stunTime);
        gameObject.tag = tag1;
        isStunned = false;
    }
}

