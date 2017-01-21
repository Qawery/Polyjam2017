using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Scenario : MonoBehaviour
{
    public GameObject standardEnemy;
    public List<AgentSpawner> spawnerList;
    private List<SquadAI> squadList;

    public void Awake()
    {
        Assert.IsNotNull(standardEnemy, "Missing standardEnemy");
        Assert.IsNotNull(spawnerList, "Missing spawnerList");
        Assert.IsFalse(spawnerList.Count <= 0, "Too little spawners");
        squadList = new List<SquadAI>();
    }
    
	public void Start ()
    {
		foreach(AgentSpawner spawner in spawnerList)
        {
            spawner.CreateBatchToSpawn(standardEnemy, 3, new Vector3(spawner.transform.position.x + 5f, spawner.transform.position.y, spawner.transform.position.z));
        }
	}

    public void Update()
    {
        CleanUpCorpses();
        foreach (SquadAI squad in squadList)
        {
            if(squad.isReady)
            {
                squad.SetDestination(new Vector3(0, 0, 0));
            }
        }
    }

    private void CleanUpCorpses()
    {
        int i = 0;
        while (i < squadList.Count)
        {
            if (squadList[i] == null)
            {
                squadList.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public void AddSquad(SquadAI newSquad)
    {
        squadList.Add(newSquad);
    }

    public List<SquadAI> GetSquadAI_List()
    {
        return squadList;
    }
}
