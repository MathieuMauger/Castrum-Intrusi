using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public static playerStats Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
