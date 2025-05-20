using System.Collections;
using UnityEngine;

public class Agarrar_Objeto : MonoBehaviour
{
    public Transform puntoDeAgarre;
    private GameObject objetoAgarrado; //Referencia al objeto que vamos a agarrar
    private bool agarrando = false;


    //Variables para el latigo
    public GameObject hitB1;
    public GameObject hitB2;
    public GameObject hitB3;
    public Transform playerTransform;
    public float timing = 0.1f;
    private bool isWhipping = false;
    private Player_Movement Player_Movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player_Movement = GetComponent<Player_Movement>();// adquiere el script para que pueda seguir el latigo al jugador 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            Debug.Log("TOY AGACHADO");

            if (agarrando)
            {
                // Soltar
                objetoAgarrado.transform.SetParent(null);
                objetoAgarrado = null;
                agarrando = false;
            }
            else
            {
                // Buscar cerca del jugador
                Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, 1.5f);
                foreach (var col in objetos)
                {
                    if (col.CompareTag("Agarrable"))
                    {
                        objetoAgarrado = col.gameObject;
                        objetoAgarrado.transform.position = puntoDeAgarre.position;
                        objetoAgarrado.transform.SetParent(puntoDeAgarre);
                        agarrando = true;
                        break;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!agarrando)
            {
                if (Input.GetKeyDown(KeyCode.X) && !isWhipping)
                {
                    StartCoroutine(WhipSequence());
                }
            }//para poner que cuendo se pique y si este agarrando algo lo lanze
        }


    }
    IEnumerator WhipSequence()
    {
        isWhipping = true;
        Player_Movement.canFlip = false; // no deja gorar mientras ataca

        bool facingRight = transform.localScale.x > 0;
        //atras 
        Vector2 offsetBack = facingRight ? new Vector2(-0.5f, 1f) : new Vector2(0.5f, 1f);
        CreateHitbox(hitB1, offsetBack);
        yield return new WaitForSeconds(timing);

        //arriba
        CreateHitbox(hitB2, new Vector2(0f, 1.5f));
        yield return new WaitForSeconds(timing);


        // ne frente 
        Vector2 offsetFront = facingRight ? new Vector2(0.5f, 1f) : new Vector2(-0.5f, 1f);
        CreateHitbox(hitB3, offsetFront);
        yield return new WaitForSeconds(timing);

        
        isWhipping = false;
        Player_Movement.canFlip = true;
    }

    void CreateHitbox(GameObject prefab, Vector2 offset)
    {
        GameObject hitbox = Instantiate(prefab, (Vector2)playerTransform.position + offset, Quaternion.identity);
        hitBoxFollow follow = hitbox.GetComponent<hitBoxFollow>();
        follow.player = playerTransform;
        follow.offset = offset;
    }
}

