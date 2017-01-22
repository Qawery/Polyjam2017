using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Scenario : MonoBehaviour
{
    public float resourcesCounter;
    public float maxResources = 100f;
    public GameObject mainTower;
    public GameObject standardEnemy;
    private List<SquadAI> squadList;

    public void Awake()
    {
        resourcesCounter = 0;
        Assert.IsNotNull(mainTower, "Missing mainTower");
        Assert.IsNotNull(standardEnemy, "Missing standardEnemy");
        squadList = new List<SquadAI>();
    }
    
	public void Start ()
    {
        mainTower.GetComponent<TowerAI>().currentPowerLevel = mainTower.GetComponent<TowerAI>().fullPowerLevel;
	}

    public void Update()
    {
        VictoryConditions();
        CleanUpCorpses();
        foreach (SquadAI squad in squadList)
        {
            if(squad.isReady)
            {
                DetermineNextTowerToAttack(squad);
            }
        }
    }

    private void VictoryConditions()
    {
        if(!mainTower.GetComponent<TowerAI>().IsPowered())
        {
            GameplayManager.GetInstance().isControllEnabled = false;
            //TODO: Przegrana
        }
        else
        {
            if(resourcesCounter >= maxResources)
            {
                //TODO: Wygrana
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
        //TODO: Wybranie wieży do zaatakowania dla oddziału
        squad.SetDestination(mainTower.transform.position);
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
        resourcesCounter += ammount;
        if(resourcesCounter > maxResources)
        {
            resourcesCounter = maxResources;
        }
    }
}
