using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    public GameObject Lever1Up;
    public GameObject Lever1Down;
    public GameObject Lever2Up;
    public GameObject Lever2Down;
    public GameObject Lever3Up;
    public GameObject Lever3Down;
    public GameObject Lever4Up;
    public GameObject Lever4Down;
    public GameObject Lever5Up;
    public GameObject Lever5Down;

    public GameObject Door1;
    public GameObject Door2;

    public GameObject activateWallPuzzle;
    public GameObject wallPuzzleCorrect;
    public GameObject lockedWall;

    private PlayerQuests playerQuests;
    private bool hasQuest = true;

    void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    void Update()
    {
        CheckDoor1();
        CheckLockedWall();
        CheckDoor2();
        UpdateQuestProgress();
    }

    public void CheckDoorsAtStart(int progress)
    {
        if (progress > 0)
        {
            Lever1Up.SetActive(false);
            Lever1Down.SetActive(true);
            Lever2Up.SetActive(false);
            Lever2Down.SetActive(true);
            Door1.SetActive(false);
        }
        if (progress > 1)
        {
            Destroy(activateWallPuzzle);
            Destroy(lockedWall);
            wallPuzzleCorrect.SetActive(true);
        }
        if (progress > 2)
        {
            Lever3Up.SetActive(false);
            Lever3Down.SetActive(true);
            Lever5Up.SetActive(false);
            Lever5Down.SetActive(true);
            Door2.SetActive(false);
        }
    }

    private void CheckDoor1()
    {
        if (Lever1Down.activeSelf && Lever2Down.activeSelf)
        {
            if (playerQuests.cs1010Progress == 0)
            {
                playerQuests.cs1010Progress = 1;
            }
            Door1.SetActive(false);
        }
        else
        {
            if (playerQuests.cs1010Progress == 1)
            {
                playerQuests.cs1010Progress = 0;
            }
            Door1.SetActive(true);
        }
    }

    private void CheckDoor2()
    {
        if ((Lever3Down.activeSelf && Lever5Down.activeSelf) ^ (Lever4Down.activeSelf && Lever5Down.activeSelf))
        {
            if (playerQuests.cs1010Progress == 2)
            {
                playerQuests.cs1010Progress = 3;
            }
            Door2.SetActive(false);
        }
        else
        {
            Door2.SetActive(true);
        }
    }

    private void CheckLockedWall()
    {
        if (playerQuests.cs1010Progress > 1)
        {
            Destroy(activateWallPuzzle);
            Destroy(lockedWall);
        }
    }

    private void UpdateQuestProgress()
    {
        if (playerQuests.cs1010Progress > 2 && hasQuest)
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == "CS1010")
                {
                    playerQuests.questList.questSlots[i].done = true;
                    Quest_UI quest_UI = GameObject.Find("Quest").GetComponent<Quest_UI>();
                    quest_UI.questSlots[i].questStatus.SetActive(true);
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
