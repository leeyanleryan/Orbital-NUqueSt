using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SUMonsterHealth : EnemyHealth
{
    public HealthBar healthBar;

    [SerializeField] private TextAsset inkJSON;

    public override void Start()
    {
        base.Start();
        healthBar = GameObject.Find("SUMonsterHealthBar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(_health);
        healthBar.SetHealth(_health);
    }

    public override float Health {
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
                Invoke(nameof(SlimeDeath), 3f);
            }
        }
        get
        {
            return _health;
        }
    }

    public override void OnHit(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        animator.SetTrigger("Hit");
    }

    private void Update()
    {
        healthBar.SetHealth(Health);
    }

    // In the arena, hides the healthbar, makes endingProgress = 1, hides the boxes blocking the path if monster dies, then plays dialogue
    public override void SlimeDeath()
    {
        if (SceneManager.GetActiveScene().name == "Arena")
        {
            GameObject.Find("SUMonsterHealthBar").SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerQuests>().endingProgress = 1;
            GameObject.Find("Blocking").SetActive(false);
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
        Destroy(this.gameObject);
    }
}
