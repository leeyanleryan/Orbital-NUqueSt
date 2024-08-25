using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour, IDataPersistence
{
    public float maxHealth = 100;
    public float health;
    public HealthBar healthBar;
    public bool hasCollided;
    public PlayerPositionSO startingPosition;

    public void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        if (startingPosition.transittedScene)
        {
            health = GameManager.instance.health;
            hasCollided = false;                        //used to ensure that u take damage exactly once when you are in aggro range
            healthBar.SetHealth(health);
            healthBar.SetMaxHealth(maxHealth);
        }
        else if (startingPosition.playerDead)
        {
            health = GameManager.instance.health;
            hasCollided = false;
            healthBar.SetHealth(health);
            healthBar.SetMaxHealth(100);
        }
        else
        {
            maxHealth = 100;
            GameManager.instance.health = DataPersistenceManager.instance.gameData.maxHealth;
            hasCollided = false;
            healthBar.SetHealth(health);
            healthBar.SetMaxHealth(maxHealth);
        }
    }
    public void Update()
    {
            GameManager.instance.health = health;
            healthBar.SetHealth(health);
        
    }

    public void LoadData(GameData data)
    {
        maxHealth = data.maxHealth;
        health = data.health;
        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);
        maxHealth = data.maxHealth;
    }

    public void SaveData(GameData data)
    {
        data.health = health;
        data.maxHealth = maxHealth;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Enemy") && !hasCollided)
        {
            health -= 10;
            healthBar.SetHealth(health);
            hasCollided = true;
        }
        else if (collision.gameObject.CompareTag("rock"))
        {
            health -= 8;
            healthBar.SetHealth(health);
        }
         else if (collision.gameObject.CompareTag("SUMonsterMelee") && !hasCollided)
        {
             health -= 15;
           healthBar.SetHealth(health);
            hasCollided = true;
         }
         else if (collision.gameObject.CompareTag("SUMonsterRock"))
         {
             health -= 10;
            healthBar.SetHealth(health);
         }
        else if (collision.gameObject.CompareTag("SUMonsterCharge"))
        {
           health -= 15;
           healthBar.SetHealth(health);
        }
        else if (collision.gameObject.CompareTag("slimeMelee"))
        {
            health -= 10;
            healthBar.SetHealth(health);
            hasCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            hasCollided = false;
        }
    }


}
