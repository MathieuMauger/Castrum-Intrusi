using UnityEngine;
using TMPro;


public class hpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        hpText.text = "HP: " + playerStats.Instance.health.ToString();
    }
}
