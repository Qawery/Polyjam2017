using System.Collections.Generic;

public class WaveInfo
{
    public float waveTime;
    public List<BatchInfo> batchesToSpawn;

    public WaveInfo()
    {
        batchesToSpawn = new List<BatchInfo>();
    }
}
