using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    public float regenerationRate;
    private bool isAlive;

    public void Update()
    {
        ApplyChange(regenerationRate * Time.deltaTime);
    }

    public void ApplyChange(float ammount)
    {
        if(isAlive)
        {
            currentHealth += ammount;
            if(currentHealth > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            if (currentHealth <= 0)
            {
                isAlive = false;
            }
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
