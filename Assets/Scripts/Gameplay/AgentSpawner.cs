using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject exampleSquadAI;
    public float spawnBoxHalfWidth = 2.5f;
    public float spawnBoxHalfHeight = 2.5f;
    public float checkSphereRadius = 1;
    private List<Vector3> possibleSpawnLocations;
    private List<BatchInfo> batchesToSpawn;

    public void Awake()
    {
        Assert.IsNotNull(exampleSquadAI, "Missing exampleSquadAI");
        possibleSpawnLocations = new List<Vector3>();
        batchesToSpawn = new List<BatchInfo>();
    }

    public void Update()
    {
        if(batchesToSpawn.Count > 0)
        {
            CheckSpawnLocations();
            CarryOnSpawning();
        }
    }

    private void CheckSpawnLocations()
    {
        possibleSpawnLocations.Clear();
        float currentZ_Offset = -spawnBoxHalfHeight;
        while(currentZ_Offset <= spawnBoxHalfHeight)
        {
            float currentX_Offset = -spawnBoxHalfWidth;
            while (currentX_Offset <= spawnBoxHalfWidth)
            {
                Collider[] collidersInRange = Physics.OverlapSphere(new Vector3(transform.position.x + currentX_Offset, transform.position.y + checkSphereRadius + 1, transform.position.z + currentZ_Offset), checkSphereRadius);
                if(collidersInRange.Length == 0)
                {
                    possibleSpawnLocations.Add(new Vector3(transform.position.x + currentX_Offset, transform.position.y, transform.position.z + currentZ_Offset));
                }
                currentX_Offset += checkSphereRadius;
            }
            currentZ_Offset += checkSphereRadius;
        }
    }

    private void CarryOnSpawning()
    {
        while(possibleSpawnLocations.Count > 0 && batchesToSpawn.Count > 0)
        {
            if(batchesToSpawn[0].remainingNumber > 0)
            {
                GameObject spawnedAgent = (GameObject) Instantiate(batchesToSpawn[0].unitType, possibleSpawnLocations[0], transform.rotation) as GameObject;
                if (spawnedAgent.GetComponent<AgentAI>().team == Teams.Enemy)
                {
                    if (batchesToSpawn[0].squadAI == null)
                    {
                        batchesToSpawn[0].squadAI = ((GameObject)Instantiate(exampleSquadAI, transform.position, transform.rotation) as GameObject).GetComponent<SquadAI>();
                        batchesToSpawn[0].squadAI.SetTeam(spawnedAgent.GetComponent<AgentAI>().team);
                    }
                    batchesToSpawn[0].squadAI.AddAgent(spawnedAgent.GetComponent<AgentAI>());
                }
                possibleSpawnLocations.RemoveAt(0);
                batchesToSpawn[0].remainingNumber--;
            }
            else
            {
                batchesToSpawn[0].squadAI.isReady = true;
                batchesToSpawn.RemoveAt(0);
            }
        }
    }

    public void CreateBatchToSpawn(BatchInfo batchInfo)
    {
        batchesToSpawn.Add(new BatchInfo(batchInfo.unitType, batchInfo.remainingNumber, batchInfo.destination));
    }

    public void CreateBatchToSpawn(GameObject unitType, int number, Vector3 destination)
    {
        batchesToSpawn.Add(new BatchInfo(unitType, number, destination));
    }
}
