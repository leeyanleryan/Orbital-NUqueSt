using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationPuzzle : WallPuzzle
{
    public int ma1512Progress;

    public int currQ;
    private int totalQ = 1;

    [SerializeField] private GameObject puzzleDoor1;

    protected override void Start()
    {
        base.Start();
        currQ = GameManager.instance.cs2040Progress + 1;
    }

    protected override void Update()
    {
        base.Update();
        UpdateQuestProgress();
    }

    protected override void ChangePuzzleText()
    {
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the population question or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nFor the inequality B^2 - 4sE < 0, where" +
            "\nB denotes per capita birth-rate of slimes, s is a small value and E is amount of slimes dying," +
            "\n\nWill the slime population grow indefinitely or slowly decrease to zero?" +
            "\n\n0. Grow" +
            "\n1. Die";
    }

    protected override int GetPuzzleAnswer()
    {
        return 1;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2(-0.1402699f, 0.7795553f), Quaternion.identity);
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
        playerQuests.ma1512Progress = ma1512Progress;
        puzzleCorrect.SetActive(true);
        puzzleTrigger.gameObject.SetActive(false);
        puzzleDoor1.SetActive(false);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= ma1512Progress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
        }
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.ma1512Progress >= ma1512Progress && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "MA1512")
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
