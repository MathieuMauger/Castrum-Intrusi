using UnityEngine;
using TMPro;


public class turnsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TurnsText;

    //public GameObject playerPrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        TurnsText.text = "Turn " + playerStats.Instance.turnCount.ToString();
    }
}
