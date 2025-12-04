using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    public static playerStats Instance;

    [Header("Health")]
    public int maxHealth = 100;
    public int health;

    [Header("Win condition")]
    public int turnCount = 0;
    public int winTurn;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            health = maxHealth;
            winTurn = 5;
            
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
            SceneManager.LoadScene("deathScreenScene");
        }
    }

    public void NextTurn()
    {
        turnCount++;
        Debug.Log("Tour : " + turnCount + "max" + winTurn);

        if (turnCount >= winTurn) {
            Win();
        } else
        {
            EnemiesSpawner.LoadRandomScene();
        }
    }

    void Win()
    {
        print("win");
        SceneManager.LoadScene("winScreenScene");
    }

    public void ResetStats()
    {
        print("health : " + health + "turncount : " + turnCount);
        health = maxHealth;
        turnCount = 0;
    }
}
