using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSumPuzzle : WallPuzzle
{
    public int ma1511Progress;

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
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the infinite sum question or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nx = 1 + 1/2 + 1/4 + 1/8 + 1/16 + ..." +
            "\nThis sequence continues infinitely, with the denominator doubling per term." +
            "\n\nWhat is the value of x?";
    }

    protected override int GetPuzzleAnswer()
    {
        return 2;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2(1.006379f, 0.7816986f), Quaternion.identity);
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
        playerQuests.ma1511Progress = ma1511Progress;
        puzzleCorrect.SetActive(true);
        puzzleTrigger.gameObject.SetActive(false);
        puzzleDoor1.SetActive(false);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= ma1511Progress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
        }
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.ma1511Progress >= ma1511Progress && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "MA1511")
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
