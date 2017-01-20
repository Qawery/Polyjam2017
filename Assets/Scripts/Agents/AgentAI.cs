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
    private AgentDefault target;

    public override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(navMeshAgent, "Missing navMeshAgent");
        turret = GetComponent<TurretAI>();
        //Assert.IsNotNull(turret, "Missing turret.");
        Idle();
    }

    public void Update()
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
    }

    private void ActionIdle()
    {

    }

    private void ActionGoToDestination()
    {

    }

    private void ActionAttackTarget()
    {

    }

    private void ActionAttackMove()
    {

    }

    private void ActionPatrol()
    {

    }

    public void Idle()
    {
        destination = transform.position;
        state = AgentInternalState.Idle;
    }

    public void GoToDestination(Vector3 newDestination)
    {
        destination = newDestination;
        navMeshAgent.SetDestination(destination);
        state = AgentInternalState.GoToDestination;
    }

    public void AttackTarget(AgentDefault newTarget)
    {
        target = newTarget;
        state = AgentInternalState.AttackTarget;
    }

    public void AttackMove(Vector3 newDestination)
    {
        destination = newDestination;
        state = AgentInternalState.AttackMove;
    }

    public void Patrol(Vector3 newDestination)
    {
        destination = newDestination;
        state = AgentInternalState.Patrol;
    }
}
