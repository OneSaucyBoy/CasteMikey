using UnityEngine;

public class cavemanLogic : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Transform player;
    private bool isActive = false;
    public LayerMask Walls;
    public Vector2 direction;
    private Rigidbody2D rb;


    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
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
    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = isActive ? runSpeed : walkSpeed;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.1f, Walls);

        if (hit.collider != null)
        {
            direction = direction == Vector2.left ? Vector2.right : Vector2.left;
            Flip(direction.x);   
        }
        else
        {
            // como lo mueve esa muchachota
        Vector2 movement = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        }
    }
    //paque se voltee
    void Flip(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}

