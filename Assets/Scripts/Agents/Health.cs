using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float regenerationRate;
    private bool isAlive = true;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        ApplyChange(regenerationRate * Time.deltaTime);
    }

    public void ApplyChange(float ammount)
    {
        if(isAlive)
        {
            currentHealth += ammount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
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
