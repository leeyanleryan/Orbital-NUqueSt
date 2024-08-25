using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;

    public PlayerQuests player;

    public bool isBeingHit;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", true);
        
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        isBeingHit = false;
    }

    public virtual float Health { 
        set
        {
            _health = value;
            if (value < 0)
            {
                animator.SetTrigger("Hit");
            }

            if (_health <= 0)
            {
                animator.SetBool("alive", false);
                ChangeListState();
                Invoke(nameof(SlimeDeath), 1.8f);
            }
        }
        get
        { 
            return _health; 
        }
    }
    public float _health;

    public virtual void OnHit(float damage)
    {
        Health -= damage;
        animator.SetTrigger("Hit");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwordAttack") && !isBeingHit)
        {
            SwordAttack swordAttack = col.gameObject.GetComponentInParent<SwordAttack>();
            OnHit(swordAttack.swordDamage);
            isBeingHit = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isBeingHit = false;
    }

    public virtual void SlimeDeath()
    {
        Destroy(this.gameObject);
    }

    public void ChangeListState()
    {
        if (GameObject.Find("EnemySpawner") != null && gameObject.transform.parent != null)
        {
            string currScene = SceneManager.GetActiveScene().name;
            EnemySpawner enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
            int enemyListIndex = enemySpawner.listOfEnemySceneNames.FindIndex(x => x == currScene);
            string enemyNameInList = gameObject.name;
            int enemyIndexInList = enemySpawner.listOfEnemyNames[enemyListIndex].FindIndex(x => x == enemyNameInList);
            enemySpawner.listOfEnemyStates[enemyListIndex][enemyIndexInList] = 0;
        }
    }
}
