﻿using System.Collections.Generic;
using UnityEngine;

public enum SquadState
{
    Deploying, Waiting, Moving, Attacking
}

public class SquadAI : MonoBehaviour
{
    private float goalTolerance = 0.5f;
    private Teams team;
    private List<AgentAI> agentsInSquad;
    private SquadState currentState;
    private Vector3 currentDestination;

    public SquadAI(AgentAI firstAgent, Vector3 deploymentDestination)
    {
        currentDestination = deploymentDestination;
        team = firstAgent.team;
        AddAgent(firstAgent);
    }

    public void Awake()
    {
        agentsInSquad = new List<AgentAI>();
    }

    public void Update()
    {
        if (agentsInSquad.Count <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if(IsUnitInCombat())
            {
                AcquireTargets();
            }
            else
            {
                if(IsDestinationReached())
                {
                    Concentrate();
                }
                else
                {
                    MoveToDestination();
                }
            }
        }
    }

    private bool IsDestinationReached()
    {
        foreach (AgentAI agent in agentsInSquad)
        {
            if (Vector3.Distance(agent.transform.position, currentDestination) <= goalTolerance)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsUnitInCombat()
    {
        foreach (AgentAI agent in agentsInSquad)
        {
            if(agent.GetCurrentTarget() != null)
            {
                return true;
            }
        }
        return false;
    }

    private void AcquireTargets()
    {
        AgentDefault randomTarget = null;
        List<AgentAI> agentsWithoutTarget = new List<AgentAI>();
        foreach (AgentAI agent in agentsInSquad)
        {
            if (agent.GetCurrentTarget() == null)
            {
                agentsWithoutTarget.Add(agent);
            }
            else
            {
                if (randomTarget == null)
                {
                    randomTarget = agent.GetCurrentTarget();
                }
            }
        }
        foreach (AgentAI agent in agentsWithoutTarget)
        {
            agent.AttackMove(randomTarget.transform.position);
        }
    }

    public void AddAgent(AgentAI newAgent)
    {
        if (newAgent.team == team && newAgent.GetHealth().IsAlive())
        {
            agentsInSquad.Add(newAgent);
        }
    }

    public void MoveToDestination()
    {
        foreach (AgentAI agent in agentsInSquad)
        {
            agent.AttackMove(currentDestination);
        }
    }

    public void Concentrate()
    {
        currentDestination = agentsInSquad[0].transform.position;
        foreach (AgentAI agent in agentsInSquad)
        {
            agent.AttackMove(currentDestination);
        }
    }

    public void SetDestination(Vector3 newDestination)
    {
        currentDestination = newDestination;
    }

    public int GetSquadSize()
    {
        return agentsInSquad.Count;
    }
}