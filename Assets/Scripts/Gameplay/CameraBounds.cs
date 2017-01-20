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

    public float getNorthBound()
    {
        return northBound;
    }

    public float getSouthBound()
    {
        return southBound;
    }

    public float getWestBound()
    {
        return westBound;
    }

    public float getEastBound()
    {
        return eastBound;
    }
}
