using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicationLogicPuzzle : WallPuzzle
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

    protected override void ChangePuzzleText()
    {
        string statement = "";
        if (randX == 0)
        {
            statement = "can Bing Chilling have 6 legs and not be an insect at the same time";
        }
        else if (randX == 1)
        {
            statement = "can Bing Chilling not have 6 legs but be an insect at the same time";
        }
        else if (randX == 2)
        {
            statement = "can Bing Chilling not have 6 legs and not be an insect at the same time";
        }
        puzzleText.text = "(1/1) Solve the implication logic statement or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nStatement Alpha: A implies B." +
            "\nA := Living thing named Bing Chilling has 6 legs." +
            "\nB := Bing Chilling is an insect." +
            "\n\nIf statement Alpha is true, " + statement +
            "\n\n0. False" +
            "\n1. True";
    }

    protected override int GetPuzzleAnswer()
    {
        if (randX == 0)
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
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)-0.935, (float)-2.22), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)-0.455, (float)-2.346), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)-0.736, (float)-2.419), Quaternion.identity);
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
}
