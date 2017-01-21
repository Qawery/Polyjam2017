using UnityEngine;

public class WeaponAI : MonoBehaviour
{
    public float range = 2f;
    public float reloadTime = 1f;
    private float reloadCooldown = 0f;

    public void Update()
    {
        if(reloadCooldown > 0)
        {
            reloadCooldown -= Time.deltaTime;
        }
    }

    public bool IsReadyToFire()
    {
        if (reloadCooldown <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsAimedAt(AgentDefault target)
    {
        //TODO czy bron jest wycelowana
        return true;
    }

    public void Fire(AgentDefault target)
    {
        //TODO efekt wystrzału
        //TODO dzwiek wystrzalu
        //TODO zadanie obrazen
    }
}
