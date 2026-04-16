using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 5f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (GetComponent<Zombie>() != null)
            {
                GameManager.Instance.ZombieDied();
            }
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
