using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfAndOnlyIfLogicPuzzle : WallPuzzle
{
    public int cs1231Progress;

    private int randX;
    public int puzzleAnswer;

    [SerializeField] private GameObject puzzleDoor1;
    [SerializeField] private GameObject puzzleDoor2;

    protected override void Start()
    {
        base.Start();
        randX = Random.Range(0, 3);
    }

    protected override void Update()
    {
        base.Update();
        UpdateQuestProgress();
    }

    protected override void ChangePuzzleText()
    {
        string statement = "";
        if (randX == 0)
        {
            statement = "can Bing Chilling be a mammal and not produce milk at the same time";
        }
        else if (randX == 1)
        {
            statement = "can Bing Chilling not be a mammal but produce milk at the same time";
        }
        else if (randX == 2)
        {
            statement = "can Bing Chilling not be a mammal and not produce milk at the same time";
        }
        puzzleText.text = "(1/1) Solve the if and only if statement or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nStatement Alpha: A if and only if B." +
            "\nA := Bing Chilling is a mammal." +
            "\nB := Bing Chilling can produce milk." +
            "\n\nIf statement Alpha is true, " + statement +
            "\n\n0. False" +
            "\n1. True";
    }

    protected override int GetPuzzleAnswer()
    {
        if (randX == 0 || randX == 1)
        {
            puzzleAnswer = 0;
        }
        else
        {
            puzzleAnswer = 1;
        }
        return puzzleAnswer;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)-0.0009991229, (float)-4.303544), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)1.413007, (float)-4.29355), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)1.592881, (float)-4.058716), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)-0.2857991, (float)-4.12367), Quaternion.identity);
    }

    protected override void CheckInBattle()
    {
        if (GameObject.Find("Slime(Clone)") == null)
        {
            inBattle = false;
            startBattle = false;
            puzzleTrigger.inBattle = false;
            puzzleTrigger.finishBattle = true;
        }
    }

    protected override void ChangeQuestProgress()
    {
        playerQuests.cs1231Progress = cs1231Progress;
        CheckQuestProgress(playerQuests.cs1231Progress);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= cs1231Progress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
            puzzleDoor2.SetActive(false);
        }
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.cs1231Progress >= cs1231Progress && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "CS1231")
                {
                    playerQuests.questList.questSlots[i].done = true;
                    hasQuest = false;
                    break;
                }
                else if (playerQuests.questList.questSlots[i].questName == null)
                {
                    hasQuest = false;
                    break;
                }
            }
        }
    }
}
