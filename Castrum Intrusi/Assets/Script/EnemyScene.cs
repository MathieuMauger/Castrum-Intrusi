using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("EnemyScene", LoadSceneMode.Additive);
    }
}