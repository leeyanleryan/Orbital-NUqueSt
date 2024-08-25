using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProductPuzzle : WallPuzzle
{
    public int ma1508EProgress;

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
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the dot product question or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nUnit vector A is perpendicular to unit vector B," +
            "\nmeaning that both vectors are of length 1 and have a 90 degree angle between them." +
            "\n\nWhat is the value of the dot product of vector A and B?";
    }

    protected override int GetPuzzleAnswer()
    {
        return 0;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2(-1.819199f, 1.480461f), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2(-0.5559641f, 1.46235f), Quaternion.identity);
    }

    protected override void CheckInBattle()
    {
        if (GameObject.Find("Slime(Clone)") == null && GameObject.Find("Skeleton(Clone)") == null)
        {
            inBattle = false;
            startBattle = false;
            puzzleTrigger.inBattle = false;
            puzzleTrigger.finishBattle = true;
        }
    }

    protected override void ChangeQuestProgress()
    {
        playerQuests.ma1508EProgress = ma1508EProgress;
        puzzleCorrect.SetActive(true);
        puzzleTrigger.gameObject.SetActive(false);
        puzzleDoor1.SetActive(false);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= ma1508EProgress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
        }
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.ma1508EProgress >= ma1508EProgress && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "MA1508E")
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
