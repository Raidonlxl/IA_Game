using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int currentHealth;
    
    public void SetMaxLife(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;

      
        if (currentHealth < 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}
