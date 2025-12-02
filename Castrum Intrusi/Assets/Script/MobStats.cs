using UnityEngine;

public class MobStats : MonoBehaviour
{

    public int mobHealth = 100;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void TakeDamage()
    {
        mobHealth -= 30;

        if (mobHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
