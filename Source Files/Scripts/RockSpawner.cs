using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockSpawner : MonoBehaviour
{
    private float rockDayChecker;
    [HideInInspector] public List<string> listOfRockSceneNames = new List<string>();
    [HideInInspector] public List<List<string>> listOfRockNames = new List<List<string>>();
    [HideInInspector] public List<List<int>> listOfRockStates = new List<List<int>>();

    private void Start()
    {
        listOfRockSceneNames = GameManager.instance.listOfRockSceneNames;
        listOfRockNames = GameManager.instance.listOfRockNames;
        listOfRockStates = GameManager.instance.listOfRockStates;
        rockDayChecker = GameManager.instance.rockDayChecker;
        string currScene = SceneManager.GetActiveScene().name;
        CheckNewDay();
        UpdateCaveList(currScene);
    }

    private void Update()
    {
        GameManager.instance.rockDayChecker = rockDayChecker;
        GameManager.instance.listOfRockSceneNames = listOfRockSceneNames;
        GameManager.instance.listOfRockNames = listOfRockNames;
        GameManager.instance.listOfRockStates = listOfRockStates;
    }

    private void CheckNewDay()
    {
        if (GameManager.instance.day > rockDayChecker)
        {
            for (int i = 0; i < listOfRockNames.Count; i++)
            {
                if (listOfRockNames[i].Count != 0)
                {
                    for (int j = 0; j < listOfRockStates[i].Count; j++)
                    {
                        listOfRockStates[i][j] = 1;
                    }
                }
            }
            rockDayChecker = GameManager.instance.day;
        }
    }

    private void UpdateCaveList(string currScene)
    {
        if (!listOfRockSceneNames.Contains(currScene))
        {
            listOfRockSceneNames.Add(currScene);
            List<string> tempListNames = new List<string>();
            List<int> tempListStates = new List<int>();
            foreach (Transform child in gameObject.transform)
            {
                tempListNames.Add(child.gameObject.name);
                tempListStates.Add(1);
            }
            listOfRockNames.Add(tempListNames);
            listOfRockStates.Add(tempListStates);
        }
        else
        {
            int index = listOfRockSceneNames.FindIndex(x => x == currScene);
            for (int i = 0; i < listOfRockNames[index].Count; i++)
            {
                int state = listOfRockStates[index][i];
                if (state == 0)
                {
                    GameObject rock = GameObject.Find(listOfRockNames[index][i]);
                    rock.SetActive(false);
                }
            }
        }
    }
}
