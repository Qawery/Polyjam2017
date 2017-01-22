using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaveManager : MonoBehaviour
{
    public GameObject standardEnemy;
    public List<AgentSpawner> northAgentSpawners;
    public List<AgentSpawner> southAgentSpawners;
    public List<AgentSpawner> westAgentSpawners;
    public List<AgentSpawner> eastAgentSpawners;

    private List<WaveInfo> waveList;
    private float waveTimer;
    private WaveInfo lastWave;

    public void Awake()
    {
        waveTimer = 0;
        Assert.IsNotNull(standardEnemy, "Missing standardEnemy");
        Assert.IsNotNull(northAgentSpawners, "Missing northAgentSpawners");
        Assert.IsTrue(northAgentSpawners.Count >= 1, "Too little northAgentSpawners");
        Assert.IsNotNull(southAgentSpawners, "Missing southAgentSpawners");
        Assert.IsTrue(southAgentSpawners.Count >= 1, "Too little southAgentSpawners");
        Assert.IsNotNull(westAgentSpawners, "Missing westAgentSpawners");
        Assert.IsTrue(westAgentSpawners.Count >= 1, "Too little westAgentSpawners");
        Assert.IsNotNull(eastAgentSpawners, "Missing eastAgentSpawners");
        Assert.IsTrue(eastAgentSpawners.Count >= 1, "Too little eastAgentSpawners");
        waveList = new List<WaveInfo>();
    }

    public void Start()
    {
        CreateWaves();
    }

    public void Update()
    {
        waveTimer += Time.deltaTime;
        if(waveList.Count > 0 && waveList[0].waveTime <= waveTimer)
        {
            waveTimer = 0;
            SpawnNextWave(waveList[0]);
            waveList.RemoveAt(0);
        }
        else if(waveList.Count == 0 && lastWave.waveTime <= waveTimer)
        {
            waveTimer = 0;
            SpawnNextWave(lastWave);
        }
    }

    private void SpawnNextWave(WaveInfo waveToSpawn)
    {
        foreach(BatchInfo batch in waveToSpawn.batchesToSpawn)
        {
            switch (batch.batchDirection)
            {
                //TODO: losowanie spawnerów
                case BatchDirection.North:
                    northAgentSpawners[0].CreateBatchToSpawn(batch);
                    break;

                case BatchDirection.South:
                    southAgentSpawners[0].CreateBatchToSpawn(batch);
                    break;

                case BatchDirection.West:
                    westAgentSpawners[0].CreateBatchToSpawn(batch);
                    break;

                case BatchDirection.East:
                    eastAgentSpawners[0].CreateBatchToSpawn(batch);
                    break;
            }
        }
    }

    private void CreateWaves()
    {
        //TODO: programistyczne zapisuwanie fal do stworzenia

        lastWave = new WaveInfo();
        lastWave.waveTime = 5f;
        BatchInfo newBatch = new BatchInfo(standardEnemy, 3, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 3, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 3, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 3, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        lastWave.batchesToSpawn.Add(newBatch);
    }
}
