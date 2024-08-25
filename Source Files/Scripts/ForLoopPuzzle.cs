using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForLoopPuzzle : WallPuzzle
{
    public int cs1010Progress;

    private int randX;
    private int randB;
    public int puzzleAnswer;

    protected override void Start()
    {
        base.Start();
        randX = Random.Range(6, 10);
        randB = Random.Range(2, 5);
    }

    protected override void ChangePuzzleText()
    {
        puzzleText.text = "(1/1) Solve the for loop to free yourself!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nint x = " + randX + ";" +
            "\nfor (int i = 0; i < " + 3 + "; i++)" +
            "\n{" +
            "\n    x = x * " + randB + " + i;" +
            "\n}" +
            "\n\nWhat is the value of x after the for loop?";
    }

    protected override int GetPuzzleAnswer()
    {
        puzzleAnswer = randX;
        int b = randB;
        for (int i = 0; i < 3; i++)
        {
            puzzleAnswer = puzzleAnswer * b + i;
        }
        return puzzleAnswer;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)0.9, (float)-2.2), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2(2, (float)-2.2), Quaternion.identity);
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
        playerQuests.cs1010Progress = cs1010Progress;
        puzzleCorrect.SetActive(true);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= cs1010Progress)
        {
            puzzleCorrect.SetActive(true);
        }
    }
}
