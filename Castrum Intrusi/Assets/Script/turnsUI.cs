using UnityEngine;
using TMPro;


public class turnsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        turnText.text = "Turn " + (playerStats.Instance.turnCount + 1).ToString();
    }
}
