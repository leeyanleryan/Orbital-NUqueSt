using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefendVillage : MonoBehaviour
{
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject townCollider;
    [SerializeField] private Slider healthBar;

    private EnemySpawner enemySpawner;
    private PlayerQuests playerQuests;

    // bools below is used to make sure the wave spawns only once
    private bool wave0;
    private bool wave1;
    private bool wave2;
    private bool wave3;
    private bool hasWon;

    private Vector2 leftPos1 = new Vector2(-8.927f, -3.237f);
    private Vector2 leftPos2 = new Vector2(-8.924f, -3.623f);
    private Vector2 topPos1 = new Vector2(-7.336539f, -1.642322f);
    private Vector2 topPos2 = new Vector2(-6.912613f, -1.642322f);
    private Vector2 botPos1 = new Vector2(-2.530537f, -7.947671f);
    private Vector2 botPos2 = new Vector2(-2.087f, -7.942f);
    
    void Start()
    {
        enemySpawner = EnemySpawner.instance;
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    // Spawns a new wave for every hour, regardless of whether the player has killed the enemies
    void Update()
    {
        if (playerQuests.endingProgress == 3)
        {
            if (!wave0 && GameManager.instance.hours == 16)
            {
                SpawnWave0();
                wave0 = true;
            }
            else if (!wave1 && GameManager.instance.hours == 17)
            {
                SpawnWave1();
                wave1 = true;
            }
            else if (!wave2 && GameManager.instance.hours == 18)
            {
                SpawnWave2();
                wave2 = true;
            }
            else if (!wave3 && GameManager.instance.hours == 19)
            {
                bossHealthBar.SetActive(true);
                Invoke(nameof(SpawnWave3), 0.1f);
                wave3 = true;
            }
        }
        else if (!hasWon && playerQuests.endingProgress == 4)
        {
            if (GameObject.Find("Slime(Clone)") == null && GameObject.Find("Skeleton(Clone)") == null
                && GameObject.Find("goblin(Clone)") == null && GameObject.Find("01(Clone)") == null)
            {
                Invoke(nameof(DefendSuccess), 2f);
                hasWon = true;
            }
        }
    }

    private void SpawnWave0()
    {
        Instantiate(enemySpawner.GetEnemyByName("Slime"), leftPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Slime"), leftPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Slime"), topPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Slime"), topPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("goblin"), leftPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("goblin"), topPos1, Quaternion.identity);
    }

    private void SpawnWave1()
    {
        Instantiate(enemySpawner.GetEnemyByName("Slime"), botPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Slime"), botPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("goblin"), topPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("goblin"), topPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), leftPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), leftPos2, Quaternion.identity);
    }

    private void SpawnWave2()
    {
        Instantiate(enemySpawner.GetEnemyByName("Slime"), botPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Slime"), leftPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("goblin"), topPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("goblin"), topPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), leftPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), leftPos2, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), botPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), botPos2, Quaternion.identity);
    }

    private void SpawnWave3()
    {
        Instantiate(enemySpawner.GetEnemyByName("goblin"), topPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), botPos1, Quaternion.identity);
        Instantiate(enemySpawner.GetEnemyByName("01"), leftPos1, Quaternion.identity);
        playerQuests.endingProgress = 4;
    }

    private void DefendSuccess()
    {
        bossHealthBar.SetActive(false);
        townCollider.SetActive(false);
        GameObject.Find("RemoveIfSuccess").SetActive(false);
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        playerQuests.endingProgress = 5;
    }
}
