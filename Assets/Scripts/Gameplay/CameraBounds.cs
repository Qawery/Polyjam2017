using UnityEngine;
using UnityEngine.Assertions;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider boundsCollider;
    private float northBound = 5f;
    private float southBound = -5f;
    private float eastBound = 5f;
    private float westBound = -5f;

    public void Awake()
    {
        //TODO wyliczyć na podstawie collidera
        Assert.IsNotNull(boundsCollider, "Missing boundsCollider");
    }

    public float GetNorthBound()
    {
        return northBound;
    }

    public float GetSouthBound()
    {
        return southBound;
    }

    public float GetWestBound()
    {
        return westBound;
    }

    public float GetEastBound()
    {
        return eastBound;
    }
}
