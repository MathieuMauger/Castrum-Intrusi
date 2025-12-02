using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    public int health = 100;
    void Awake()
    {
        
    }

    void Update()
    {

        if (health <= 0)
        {
            
            Destroy(gameObject);
            print("Dead");
            SceneManager.LoadScene("deathScreenScene");
        }
        
    }


}
