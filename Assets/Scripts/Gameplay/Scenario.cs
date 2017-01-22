using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Scenario : MonoBehaviour
{
    public float resourcesCounter;
    public float maxResources = 100f;
    public GameObject mainTower;
    private List<SquadAI> squadList;
    private bool ending;
    private float transitionCountdown = 5f;

    public void Awake()
    {
        ending = false;
        resourcesCounter = maxResources/2;
        Assert.IsNotNull(mainTower, "Missing mainTower");
        squadList = new List<SquadAI>();
    }
    
	public void Start ()
    {
        mainTower.GetComponent<TowerAI>().currentPowerLevel = mainTower.GetComponent<TowerAI>().fullPowerLevel;
	}

    public void Update()
    {
        EndingConditions();
        CleanUpCorpses();
        foreach (SquadAI squad in squadList)
        {
            if(squad != null && squad.isReady && squad.IsDestinationReached() && !squad.IsUnitInCombat())
            {
                DetermineNextTowerToAttack(squad);
            }
        }
    }

    private void EndingConditions()
    {
        if (!ending)
        {
            if (resourcesCounter <= 0 || mainTower.GetComponent<TowerAI>().currentPowerLevel <= 0f)
            {
                GameplayManager.GetInstance().isControllEnabled = false;
                ending = true;
                GameplayManager.GetInstance().gui.ShowEndingText(false);
            }
            else if (resourcesCounter >= maxResources)
            {
                ending = true;
                GameplayManager.GetInstance().gui.ShowEndingText(true);
            }
            IncreaseResources(-5 * Time.deltaTime);
        }
        else
        {
            transitionCountdown -= Time.deltaTime;
            if(transitionCountdown <= 0)
            {
                Application.LoadLevel("MainMenu");
            }
        }
    }

    private void CleanUpCorpses()
    {
        int i = 0;
        while (i < squadList.Count)
        {
            if (squadList[i] == null)
            {
                squadList.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    private void DetermineNextTowerToAttack(SquadAI squad)
    {
        List<TowerAI> towersLeft = new List<TowerAI>();
        foreach (TowerAI tower in GameplayManager.GetInstance().towerList)
        {
            if (tower.IsPowered())
            {
                towersLeft.Add(tower);
            }
        }
        if (towersLeft.Count > 0)
        {
            float random = Random.Range(0, towersLeft.Count - 0.1f);
            int index = (int)Mathf.Floor(random);
            squad.SetDestination(towersLeft[index].transform.position);
        }
    }

    public void AddSquad(SquadAI newSquad)
    {
        squadList.Add(newSquad);
    }

    public List<SquadAI> GetSquadAI_List()
    {
        return squadList;
    }

    public void IncreaseResources(float ammount)
    {
        if (!ending)
        {
            resourcesCounter += ammount;
            if (resourcesCounter > maxResources)
            {
                resourcesCounter = maxResources;
            }
        }
    }
}
