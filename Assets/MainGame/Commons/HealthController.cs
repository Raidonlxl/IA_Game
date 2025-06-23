using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int currentHealth;
    private int maxHealth;
    public void SetMaxLife(int maxHealth)
    {
        this.maxHealth = maxHealth;
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
    public void GetHeal()
    {
        currentHealth = maxHealth;
    }
}
