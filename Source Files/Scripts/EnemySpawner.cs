using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    private Dictionary<string, GameObject> nameToEnemyDict = new Dictionary<string, GameObject>();
    public static EnemySpawner instance;

    private float enemyDayChecker;
    [HideInInspector] public List<string> listOfEnemySceneNames = new List<string>();
    [HideInInspector] public List<List<string>> listOfEnemyNames = new List<List<string>>();
    [HideInInspector] public List<List<int>> listOfEnemyStates = new List<List<int>>();

    private void Start()
    {
        instance = this;
        foreach (GameObject enemy in enemies)
        {
            AddItem(enemy);
        }

        listOfEnemySceneNames = GameManager.instance.listOfEnemySceneNames;
        listOfEnemyNames = GameManager.instance.listOfEnemyNames;
        listOfEnemyStates = GameManager.instance.listOfEnemyStates;
        enemyDayChecker = GameManager.instance.enemyDayChecker;
        string currScene = SceneManager.GetActiveScene().name;
        CheckNewDay();
        UpdateEnemyList(currScene);
    }

    private void AddItem(GameObject enemy)
    {
        if (!nameToEnemyDict.ContainsKey(enemy.name))
        {
            nameToEnemyDict.Add(enemy.name, enemy);
        }
    }

    public GameObject GetEnemyByName(string key)
    {
        if (nameToEnemyDict.ContainsKey(key))
        {
            return nameToEnemyDict[key];
        }
        return null;
    }

    private void Update()
    {
        GameManager.instance.enemyDayChecker = enemyDayChecker;
        GameManager.instance.listOfEnemySceneNames = listOfEnemySceneNames;
        GameManager.instance.listOfEnemyNames = listOfEnemyNames;
        GameManager.instance.listOfEnemyStates = listOfEnemyStates;
    }

    private void CheckNewDay()
    {
        if (GameManager.instance.day > enemyDayChecker)
        {
            for (int i = 0; i < listOfEnemyNames.Count; i++)
            {
                if (listOfEnemyNames[i].Count != 0)
                {
                    for (int j = 0; j < listOfEnemyStates[i].Count; j++)
                    {
                        listOfEnemyStates[i][j] = 1;
                    }
                }
            }
            enemyDayChecker = GameManager.instance.day;
        }
    }

    private void UpdateEnemyList(string currScene)
    {
        if (!listOfEnemySceneNames.Contains(currScene))
        {
            listOfEnemySceneNames.Add(currScene);
            List<string> tempListNames = new List<string>();
            List<int> tempListStates = new List<int>();
            foreach (Transform child in gameObject.transform)
            {
                tempListNames.Add(child.gameObject.name);
                tempListStates.Add(1);
            }
            listOfEnemyNames.Add(tempListNames);
            listOfEnemyStates.Add(tempListStates);
        }
        else
        {
            int index = listOfEnemySceneNames.FindIndex(x => x == currScene);
            for (int i = 0; i < listOfEnemyNames[index].Count; i++)
            {
                int state = listOfEnemyStates[index][i];
                if (state == 0)
                {
                    GameObject enemy = GameObject.Find(listOfEnemyNames[index][i]);
                    enemy.SetActive(false);
                }
            }
        }
    }
}
