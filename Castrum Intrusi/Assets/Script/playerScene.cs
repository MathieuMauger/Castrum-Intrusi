using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("playerScene", LoadSceneMode.Additive);
    }
}