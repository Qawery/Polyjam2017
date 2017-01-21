using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public enum AgentInternalState
{
    Idle, GoToDestination, AttackTarget, AttackMove, Patrol
}

public class AgentAI : AgentDefault
{
    private float distanceToleration = 3f;
    private NavMeshAgent navMeshAgent;
    private TurretAI turret;
    private AgentInternalState state;
    private Vector3 destination;
    private Vector3 patrolDestination;
    private bool toPatrolDestination = false;

    public override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(navMeshAgent, "Missing navMeshAgent");
        turret = GetComponentInChildren<TurretAI>();
        Assert.IsNotNull(turret, "Missing turret.");
        Idle();
    }

    public void Update()
    {
        if (health.IsAlive())
        {
            turret.ManualUpdate();
            switch (state)
            {
                case AgentInternalState.Idle:
                    break;

                case AgentInternalState.GoToDestination:
                    ActionGoToDestination();
                    break;

                case AgentInternalState.AttackTarget:
                    ActionAttackTarget();
                    break;

                case AgentInternalState.AttackMove:
                    ActionAttackMove();
                    break;

                case AgentInternalState.Patrol:
                    ActionPatrol();
                    break;
            }
        }
        else
        {
            //TODO śmierć postaci
            Destroy(gameObject);
        }
    }

    private void ActionGoToDestination()
    {
        if(Vector3.Distance(transform.position, destination) < distanceToleration)
        {
            Idle();
        }
    }

    private void ActionAttackTarget()
    {
        if(turret.GetPriorityTarget() == null || turret.GetPriorityTarget().GetHealth() == null || !turret.GetPriorityTarget().GetHealth().IsAlive())
        {
            Idle();
        }
        else
        {
            if (turret.IsInRange(turret.GetPriorityTarget()))
            {
                destination = transform.position;
            }
            else
            {
                destination = turret.GetPriorityTarget().transform.position;
            }
            navMeshAgent.SetDestination(destination);
        }
    }

    private void ActionAttackMove()
    {
        if(turret.GetTemporaryTarget() != null)
        {
            navMeshAgent.SetDestination(transform.position);
        }
        else
        {
            navMeshAgent.SetDestination(destination);
        }
    }

    private void ActionPatrol()
    {
        if (turret.GetTemporaryTarget() != null)
        {
            navMeshAgent.SetDestination(transform.position);
        }
        else
        {
            if (toPatrolDestination)
            {
                navMeshAgent.SetDestination(patrolDestination);
                if (Vector3.Distance(transform.position, patrolDestination) <= distanceToleration)
                {
                    toPatrolDestination = false;
                }
            }
            else
            {
                navMeshAgent.SetDestination(destination);
                if (Vector3.Distance(transform.position, destination) <= distanceToleration)
                {
                    toPatrolDestination = true;
                }
            }
        }
    }

    public void Idle()
    {
        if (health.IsAlive())
        {
            destination = transform.position;
            turret.SetPriorityTarget(null);
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.Idle;
        }
    }

    public void GoToDestination(Vector3 newDestination)
    {
        if (health.IsAlive())
        {
            destination = newDestination;
            turret.SetPriorityTarget(null);
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.GoToDestination;
        }
    }

    public void AttackTarget(AgentDefault newTarget)
    {
        if (health.IsAlive())
        {
            turret.SetPriorityTarget(newTarget);
            destination = newTarget.transform.position;
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.AttackTarget;
        }
    }

    public void AttackMove(Vector3 newDestination)
    {
        if (health.IsAlive())
        {
            destination = newDestination;
            turret.SetPriorityTarget(null);
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.AttackMove;
        }
    }

    public void Patrol(Vector3 newDestination)
    {
        if (health.IsAlive())
        {
            destination = transform.position;
            patrolDestination = newDestination;
            turret.SetPriorityTarget(null);
            navMeshAgent.SetDestination(patrolDestination);
            toPatrolDestination = true;
            state = AgentInternalState.Patrol;
        }
    }

    public override bool IsAvailableToSelect()
    {
        if(health.IsAlive() && team == Teams.Player)
        {
            //TODO sprawdzanie zasięgu radiowego
            return true;
        }
        return false;
    }

    public AgentDefault GetCurrentTarget()
    {
        return turret.GetTemporaryTarget();
    }
}
