using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaveManager : MonoBehaviour
{
    public GameObject standardEnemy;
    public GameObject fastEnemy;
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
        Assert.IsNotNull(fastEnemy, "Missing fastEnemy");
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
            float random;
            int index;
            switch (batch.batchDirection)
            {
                case BatchDirection.North:
                    random = Random.Range(0, northAgentSpawners.Count - 0.1f);
                    index = (int)Mathf.Floor(random);
                    northAgentSpawners[index].CreateBatchToSpawn(batch);
                    break;

                case BatchDirection.South:
                    random = Random.Range(0, southAgentSpawners.Count - 0.1f);
                    index = (int)Mathf.Floor(random);
                    southAgentSpawners[index].CreateBatchToSpawn(batch);
                    break;

                case BatchDirection.West:
                    random = Random.Range(0, westAgentSpawners.Count - 0.1f);
                    index = (int)Mathf.Floor(random);
                    westAgentSpawners[index].CreateBatchToSpawn(batch);
                    break;

                case BatchDirection.East:
                    random = Random.Range(0, eastAgentSpawners.Count - 0.1f);
                    index = (int)Mathf.Floor(random);
                    eastAgentSpawners[index].CreateBatchToSpawn(batch);
                    break;
            }
        }
    }

    private void CreateWaves()
    {
        BatchInfo newBatch;
        WaveInfo newWave;

        //Pierwsza fala
        newWave = new WaveInfo();
        newWave.waveTime = 2f;

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        newWave.batchesToSpawn.Add(newBatch);

        waveList.Add(newWave);

        //Druga
        newWave = new WaveInfo();
        newWave.waveTime = 4f;

        newBatch = new BatchInfo(fastEnemy, 2, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(fastEnemy, 2, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        newWave.batchesToSpawn.Add(newBatch);

        waveList.Add(newWave);

        //Trzecia
        newWave = new WaveInfo();
        newWave.waveTime = 5f;

        newBatch = new BatchInfo(fastEnemy, 4, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(fastEnemy, 4, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        newWave.batchesToSpawn.Add(newBatch);

        waveList.Add(newWave);

        //Czwarta
        lastWave = new WaveInfo();
        lastWave.waveTime = 5f;

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(fastEnemy, 3, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        newWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        newWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(fastEnemy, 3, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        newWave.batchesToSpawn.Add(newBatch);

        waveList.Add(newWave);

        //Ostatnia fala
        lastWave = new WaveInfo();
        lastWave.waveTime = 5f;

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, -5));
        newBatch.batchDirection = BatchDirection.North;
        lastWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        lastWave.batchesToSpawn.Add(newBatch);
        newBatch = new BatchInfo(standardEnemy, 1, new Vector3(0, 0, 5));
        newBatch.batchDirection = BatchDirection.South;
        lastWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(fastEnemy, 4, new Vector3(-5, 0, 0));
        newBatch.batchDirection = BatchDirection.East;
        lastWave.batchesToSpawn.Add(newBatch);

        newBatch = new BatchInfo(fastEnemy, 4, new Vector3(5, 0, 0));
        newBatch.batchDirection = BatchDirection.West;
        lastWave.batchesToSpawn.Add(newBatch);
    }
}
