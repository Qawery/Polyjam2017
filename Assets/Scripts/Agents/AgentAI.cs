using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public enum AgentInternalState
{
    Idle, GoToDestination, AttackTarget, AttackMove, Patrol
}

public class AgentAI : AgentDefault
{
    public float distanceToleration = 0.5f;
    private NavMeshAgent navMeshAgent;
    private TurretAI turret;
    private AgentInternalState state;
    private Vector3 destination;

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

                default:
                    Assert.IsFalse(true, "Not supported agent state.");
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
        //TODO akcja
    }

    private void ActionPatrol()
    {
        //TODO akcja
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
            destination = newDestination;
            turret.SetPriorityTarget(null);
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.Patrol;
        }
    }
}
