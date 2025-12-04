using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{

    public void launchGame()
    {
        EnemiesSpawner.LoadRandomScene();
    }

    public void leaveGame()
    {
        Debug.Log("Leave the game");
        Application.Quit();
    }
}
