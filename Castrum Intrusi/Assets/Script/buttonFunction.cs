using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{

    public void launchGame()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void leaveGame()
    {
        Debug.Log("Leave the game");
        Application.Quit();
    }
}
