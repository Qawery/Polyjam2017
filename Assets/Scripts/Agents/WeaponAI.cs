﻿using UnityEngine;
using UnityEngine.Assertions;

public class WeaponAI : MonoBehaviour
{
    public float range = 2f;
    public float damage = 1f;
    public float reloadTime = 1f;
    private float reloadCooldown = 0f;

    public void Start()
    {
    }

    public void Update()
    {
        if(reloadCooldown > 0)
        {
            reloadCooldown -= Time.deltaTime;
        }
    }

    public bool IsReadyToFire()
    {
        if (reloadCooldown <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsAimedAt(AgentDefault target)
    {
        return true;
    }

    public void Fire(AgentDefault target)
    {
        //TODO efekt wystrzału
        //TODO dzwiek wystrzalu
        if(IsReadyToFire())
        {
            reloadCooldown = reloadTime;
            target.GetHealth().ApplyChange(-damage);
            if(GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
