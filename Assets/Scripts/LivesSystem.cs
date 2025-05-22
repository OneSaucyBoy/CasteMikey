using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    public float lives = 4f;
    private bool isAlive = true;
    public GameObject gameoverUI;

    void Update()
    {
        if (!isAlive)
            GameOver();
        if (lives <= 0)
            {
                isAlive = false;
            }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            lives -= 1;
        }
    }
     void GameOver()
    {
        Debug.Log("moristes");
        Time.timeScale = 0f; 
         if (gameoverUI != null)
            gameoverUI.SetActive(true); 
        else
            Debug.LogWarning("gameoverUI no está asignado en el inspector.");
    }
 }


