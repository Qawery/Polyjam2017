using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public enum AgentInternalState
{
    Idle, GoToDestination, AttackTarget, AttackMove, Patrol
}

public class AgentAI : AgentDefault
{
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
            switch (state)
            {
                case AgentInternalState.Idle:
                    ActionIdle();
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
            turret.ManualUpdate();
        }
        else
        {
            //TODO śmierć postaci
        }
    }

    private void ActionIdle()
    {
        //TODO akcja
    }

    private void ActionGoToDestination()
    {
        //TODO akcja
    }

    private void ActionAttackTarget()
    {
        //TODO akcja
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
            turret.priorityTarget = null;
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.Idle;
        }
    }

    public void GoToDestination(Vector3 newDestination)
    {
        if (health.IsAlive())
        {
            destination = newDestination;
            turret.priorityTarget = null;
            navMeshAgent.SetDestination(destination);
            state = AgentInternalState.GoToDestination;
        }
    }

    public void AttackTarget(AgentDefault newTarget)
    {
        if (health.IsAlive())
        {
            turret.priorityTarget = newTarget;
            state = AgentInternalState.AttackTarget;
        }
    }

    public void AttackMove(Vector3 newDestination)
    {
        if (health.IsAlive())
        {
            destination = newDestination;
            turret.priorityTarget = null;
            state = AgentInternalState.AttackMove;
        }
    }

    public void Patrol(Vector3 newDestination)
    {
        if (health.IsAlive())
        {
            destination = newDestination;
            turret.priorityTarget = null;
            state = AgentInternalState.Patrol;
        }
    }
}
