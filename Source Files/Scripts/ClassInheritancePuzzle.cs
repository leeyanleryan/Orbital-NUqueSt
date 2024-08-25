using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassInheritancePuzzle : WallPuzzle
{
    public int cs2030Progress;

    private int randX;
    public int puzzleAnswer;

    private int currQ = 1;
    private int totalQ = 2;

    [SerializeField] private GameObject puzzleDoor1;
    [SerializeField] private GameObject puzzleDoor2;

    protected override void Start()
    {
        base.Start();
        randX = Random.Range(0, 4);
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
            statement = "Which of the following objects are new and present in both of the inherited classes?" +
                "\n\n1. Bed" +
                "\n2. Wardrobe" +
                "\n3. Vase" +
                "\n4. Chair" +
                "\n5. Potted Plant";
        }
        else if (randX == 1)
        {
            statement = "Which of the following objects are unique to the inherited class BedRoom?" +
                "\n\n1. Bed" +
                "\n2. Wardrobe" +
                "\n3. Chair" +
                "\n4. Options 1 and 2" +
                "\n5. Options 1, 2 and 3";
        }
        else if (randX == 2)
        {
            statement = "Which of the following objects are unique to the inherited class Kitchen?" +
                "\n\n1. Potted Plant" +
                "\n2. Sink" +
                "\n3. Refrigerator" +
                "\n4. Options 1 and 2" +
                "\n5. Options 2 and 3";
        }
        else if (randX == 3)
        {
            statement = "Which of the following objects are present in all classes?" +
                "\n\n1. Wardrobe" +
                "\n2. Chair" +
                "\n3. Refrigerator" +
                "\n4. Note" +
                "\n5. Sink";
        }
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the class inheritance question or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\n" + statement;
    }

    protected override int GetPuzzleAnswer()
    {
        if (randX == 0 || randX == 1 |randX == 3)
        {
            puzzleAnswer = 4;
        }
        else if (randX == 2)
        {
            puzzleAnswer = 5;
        }
        return puzzleAnswer;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)-0.009534121, (float)-0.7193651), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)0.4735398, (float)-0.8440293), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)0.9670025, (float)-0.7972803), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)-0.09783792, (float)-0.173959), Quaternion.identity);
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
        playerQuests.cs2030Progress = cs2030Progress;
        puzzleCorrect.SetActive(true);
        puzzleTrigger.gameObject.SetActive(false);
        puzzleDoor1.SetActive(false);
        puzzleDoor2.SetActive(true);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= cs2030Progress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
            puzzleDoor2.SetActive(true);
        }
    }

    public override void CheckAnswer()
    {
        bool parseSuccess = int.TryParse(puzzleInput.text.Trim(), out int playerAnswer);
        if (parseSuccess)
        {
            if (playerAnswer == GetPuzzleAnswer())
            {
                if (currQ == totalQ)
                {
                    puzzleText.text = "Correct!\n\nYou are now freed from this room.";
                    puzzleClose.SetActive(true);
                    puzzleInput.gameObject.SetActive(false);
                    ChangeQuestProgress();
                }
                else
                {
                    int prevX = randX;
                    currQ++;
                    do
                    {
                        randX = Random.Range(0, 4);
                    } 
                    while (randX == prevX);
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
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.cs2030Progress >= cs2030Progress && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "CS2030")
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
