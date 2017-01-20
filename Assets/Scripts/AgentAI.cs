using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class AgentAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(navMeshAgent, "Missing navMeshAgent");
    }

    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}
