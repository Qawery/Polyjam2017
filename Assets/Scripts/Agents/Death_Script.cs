using UnityEngine;

public class Death_Script : MonoBehaviour
{
    private float timer;

	public void Start ()
    {
        timer = 0f;
	}

    public void Update ()
    {
        timer += Time.deltaTime;      	
        if(timer > 3f)
        {
            Destroy(gameObject);
        }
	}
}
