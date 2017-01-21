using UnityEngine;
using UnityEngine.Assertions;

public class TurretAI : MonoBehaviour
{
    private AgentDefault owner;
    private WeaponAI weapon;
    private AgentDefault priorityTarget;
    private AgentDefault temporaryTarget;

    public void Awake()
    {
        weapon = GetComponent<WeaponAI>();
        Assert.IsNotNull(weapon, "Missing weapon");
        owner = GetComponentInParent<AgentDefault>();
        Assert.IsNotNull(owner, "Missing owner");
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

    public bool IsInRange(AgentDefault target)
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= weapon.range)
        {
            return true;
        }
        return false;
    }

    private void AimAt(AgentDefault target)
    {
        Vector3 aimDirection = target.transform.position - transform.position;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, aimDirection, 2*Mathf.PI, 0f);
        transform.localRotation = Quaternion.LookRotation(newRotation);
    }

    private void EngageTarget(AgentDefault target)
    {
        AimAt(target);
        if(weapon.IsAimedAt(target))
        {
            weapon.Fire(target);
        }
    }

    private AgentDefault FindTarget()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, weapon.range);
        foreach(Collider collider in collidersInRange)
        {
            AgentDefault agent = collider.GetComponent<AgentDefault>();
            if(agent != null && agent != owner && agent.team != owner.team && agent.GetHealth().IsAlive())
            {
                return agent;
            }
        }
        return null;
    }

    public void SetPriorityTarget(AgentDefault newPriorityTarget)
    {
        if(newPriorityTarget != owner)
        {
            priorityTarget = newPriorityTarget;
        }
    }

    public AgentDefault GetPriorityTarget()
    {
        return priorityTarget;
    }

    public AgentDefault GetTemporaryTarget()
    {
        return temporaryTarget;
    }
}
