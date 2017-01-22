using UnityEngine;
using UnityEngine.Assertions;

public class TowerAI : MonoBehaviour
{
    public GameObject powered;
    public GameObject contested;
    public GameObject rising;
    public GameObject lowering;

    public float takeOverRadius = 10f;
    public float controllPulseRange = 30f;
    public float fullPowerLevel = 5f;
    public float currentPowerLevel = 0f;

    public void Awake()
    {
        Assert.IsNotNull(powered, "Missing powered");
        Assert.IsNotNull(contested, "Missing contested");
        Assert.IsNotNull(rising, "Missing rising");
        Assert.IsNotNull(lowering, "Missing lowering");
    }

    public void Start()
    {
        GameplayManager.GetInstance().towerList.Add(this);
    }

    public void Update()
    {
        powered.SetActive(false);
        rising.SetActive(false);
        lowering.SetActive(false);
        contested.SetActive(false);
        AnalyzeControll();
        if (currentPowerLevel >= fullPowerLevel)
        {
            //TODO: włączyć efekty zasilania
            GameplayManager.GetInstance().scenario.IncreaseResources(Time.deltaTime);
            ControllPulse();
            powered.SetActive(true);
        }
        else
        {
            //TODO: wyłączyć efekty zasilania
        }
    }

    private void AnalyzeControll()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, takeOverRadius);
        bool playerPresent = false;
        bool enemyPresent = false;
        foreach (Collider collider in collidersInRange)
        {
            if (collider.GetComponent<AgentAI>() != null)
            {
                if (collider.GetComponent<AgentAI>().team == Teams.Player)
                {
                    playerPresent = true;
                }
                else if (collider.GetComponent<AgentAI>().team == Teams.Enemy)
                {
                    enemyPresent = true;
                }
            }
        }
        if (playerPresent && !enemyPresent)
        {
            ChangePowerLevel(Time.deltaTime);
            rising.SetActive(true);
        }
        else if (!playerPresent && enemyPresent)
        {
            ChangePowerLevel(-Time.deltaTime);
            lowering.SetActive(true);
        }
        else if (playerPresent && enemyPresent)
        {
            contested.SetActive(true);
        }
    }

    private void ControllPulse()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, controllPulseRange);
        foreach (Collider collider in collidersInRange)
        {
            if (collider.GetComponent<AgentAI>() != null && collider.GetComponent<AgentAI>().team == Teams.Player)
            {
                collider.GetComponent<AgentAI>().RecieveControllPulse();
            }
        }
    }

    private void ChangePowerLevel(float change)
    {
        currentPowerLevel += change;
        if (currentPowerLevel > fullPowerLevel)
        {
            currentPowerLevel = fullPowerLevel;
        }
        else if (currentPowerLevel < 0)
        {
            currentPowerLevel = 0;
        }
    }

    public bool IsPowered()
    {
        if (currentPowerLevel >= fullPowerLevel)
        {
            return true;
        }
        return false;
    }
}
