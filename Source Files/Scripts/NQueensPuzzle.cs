using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NQueensPuzzle : WallPuzzle
{
    public int cs2040Progress;

    private QueenChecker queenChecker;

    public int currQ;
    private int totalQ = 4;

    [SerializeField] private GameObject puzzleDoor1;

    protected override void Start()
    {
        base.Start();
        queenChecker = GameObject.Find("QueenPieces").GetComponent<QueenChecker>();
        currQ = GameManager.instance.cs2040Progress + 2;
    }

    protected override void Update()
    {
        base.Update();
        UpdateQuestProgress();
    }

    protected override void ChangePuzzleText()
    {
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the n-Queens puzzle or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nHold a weapon and attack a queen to push it." +
            "\nYou can only move the queen left and right." +
            "\n\nIf you think your configuration is correct, click OK." +
            "\n\nYou have to solve this " + (totalQ - currQ + 1) + " more times!";
    }

    protected override int GetPuzzleAnswer()
    {
        if (queenChecker.CheckX())
        {
            return 0;
        }
        foreach (Vector2Int queenPosition in queenChecker.queenPositions)
        {
            if (queenChecker.CheckDiag(queenPosition))
            {
                return 0;
            }
        }
        return 1;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)0.318, (float)0.148), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)0.486, (float)-0.484), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)1.677, (float)-0.322), Quaternion.identity);
    }

    protected override void CheckInBattle()
    {
        if (GameObject.Find("Skeleton(Clone)") == null)
        {
            inBattle = false;
            startBattle = false;
            puzzleTrigger.inBattle = false;
            puzzleTrigger.finishBattle = true;
        }
    }

    protected override void ChangeQuestProgress()
    {
        playerQuests.cs2040Progress = cs2040Progress;
        puzzleCorrect.SetActive(true);
        puzzleTrigger.gameObject.SetActive(false);
        puzzleDoor1.SetActive(false);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= cs2040Progress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
        }
    }

    public override void CheckAnswer()
    {
        if (GetPuzzleAnswer() == 1)
        {
            if (queenChecker.HasSeenBefore())
            {
                puzzleText.text = "(" + currQ + "/" + totalQ + ") You have tried this configuration already.\n\nTry again!";
                puzzleClose.SetActive(true);
            }
            else if (currQ == totalQ)
            {
                puzzleText.text = "Correct!\n\nYou are now freed from this room.";
                puzzleClose.SetActive(true);
                puzzleInput.gameObject.SetActive(false);
                ChangeQuestProgress();
            }
            else
            {
                playerQuests.cs2040Progress++;
                currQ++;
                GetPuzzleAnswer();
                ChangePuzzleText();
            }
        }
        else
        {
            puzzleText.text = "Oh no, that is wrong...\n\nThere is a surprise waiting for you :)";
            puzzleClose.SetActive(true);
            puzzleInput.gameObject.SetActive(false);
            startBattle = true;
        }
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.cs2040Progress >= cs2040Progress && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "CS2040")
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
