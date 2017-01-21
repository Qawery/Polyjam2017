using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private WeaponAI weapon;

    public AgentDefault priorityTarget;
    private AgentDefault temporaryTarget;

    public void Awake()
    {
        //TODO: znalezienie broni
    }

    public void ManualUpdate()
    {
        if(priorityTarget != null && IsInRange(priorityTarget))
        {
            EngageTarget(priorityTarget);
        }
        else if(temporaryTarget != null && IsInRange(temporaryTarget))
        {
            EngageTarget(temporaryTarget);
        }
        else
        {
            temporaryTarget = FindTarget();
            if(temporaryTarget != null)
            {
                EngageTarget(temporaryTarget);
            }
        }
    }

    private bool IsInRange(AgentDefault target)
    {
        //TODO: sprawwdzenie odleglosci i zasięgu broni
        return false;
    }

    private void AimAt(AgentDefault target)
    {
        //TODO celowanie wieżyczką do celu
    }

    private void EngageTarget(AgentDefault target)
    {
        AimAt(target);
        if(weapon.IsAimedAt(target) && weapon.IsReadyToFire())
        {
            weapon.Fire(target);
        }
    }

    private AgentDefault FindTarget()
    {
        //TODO: zwrócenie pierwszego poprawnego celu
        return null;
    }
}
