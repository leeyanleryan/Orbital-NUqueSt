using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy = 100;
    public float energy = 100;
    public EnergyBar energyBar;
    public PlayerPositionSO startingPosition;
    public bool hasAttacked;
    public float timer;
    public float timerRegen;

    public void Start()
    {
        energyBar = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
        if (startingPosition.transittedScene)
        {
            energy = GameManager.instance.energy;                 
            energyBar.SetHealth(energy);
            energyBar.SetMaxHealth(maxEnergy);
        }
        else if (startingPosition.playerDead)
        {
            energy = GameManager.instance.energy;
            energyBar.SetHealth(energy);
            energyBar.SetMaxHealth(100);
        }
        else
        {
            maxEnergy = 100;
            GameManager.instance.energy = 100;
            energyBar.SetHealth(energy);
            energyBar.SetMaxHealth(maxEnergy);
        }
    }
    public void Update()
    {
        if (energy >= 100)
        {
            energy = 100;
        }
        GameManager.instance.energy = energy;
        energyBar.SetHealth(energy);
        
        if (hasAttacked)
        {
            timer += Time.fixedDeltaTime;
            timerRegen = 0;
        }
        if (timer > 1f && hasAttacked)
        {
            hasAttacked = false;
            timer = 0;
        }
        if (!hasAttacked && energy <= 100)
        {
            energy += Time.fixedDeltaTime * 12f;
        }
    }
}
