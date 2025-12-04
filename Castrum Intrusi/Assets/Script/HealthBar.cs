using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform bar;
    [SerializeField] private playerStats player;

    private float maxWidth;

    void Awake()
    {
         Debug.Log("HealthBar Awake appelé");
        
            maxWidth = bar.sizeDelta.x; 
            Debug.Log(maxWidth);
    }

    void Update()
    {
         Debug.Log("HealthBar Update appelé");
        
            float healthRatio = (float)player.health / player.maxHealth;
            bar.sizeDelta = new Vector2(maxWidth * healthRatio, bar.sizeDelta.y);
            Debug.Log(healthRatio);
        
    }
}
