using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public void Awake()
    {
        Destroy(gameObject);
    }
}
