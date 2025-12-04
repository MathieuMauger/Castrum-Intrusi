using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    private float Health, MaxHealth, Width, Height;

    void Awake()
    {
        if (healthBar != null)
        {
            Width = healthBar.sizeDelta.x;
            Height = healthBar.sizeDelta.y;
        }
    }

    public void setMaxHealth(float health)
    {
        MaxHealth = health;
        setHealth(health);
    }

    public void setHealth(float health)
    {
        Health = health;
        if (healthBar != null && MaxHealth > 0)
        {
            float newWidth = (Health / MaxHealth) * Width;
            healthBar.sizeDelta = new Vector2(newWidth, Height);
        }
    }
}
