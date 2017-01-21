using UnityEngine;
using UnityEngine.Assertions;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider boundsCollider;
    private float northBound;
    private float southBound;
    private float eastBound;
    private float westBound;

    public void Start()
    {
        Assert.IsNotNull(boundsCollider, "Missing boundsCollider");
        northBound = transform.position.z + ((boundsCollider.size.z*transform.localScale.z) / 2) - GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize;
        southBound = transform.position.z - ((boundsCollider.size.z*transform.localScale.z) / 2) + GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize;
        if (northBound < southBound)
        {
            northBound = transform.position.z;
            southBound = transform.position.z;
        }
        eastBound = transform.position.x + ((boundsCollider.size.x * transform.localScale.x) / 2) - GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize;
        westBound = transform.position.x - ((boundsCollider.size.x * transform.localScale.x) / 2) + GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize;
        if (eastBound < westBound)
        {
            eastBound = transform.position.x;
            westBound = transform.position.x;
        }
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
