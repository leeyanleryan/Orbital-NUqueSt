using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPuzzleTrigger : MonoBehaviour
{
    public GameObject wallPuzzleTrigger;
    public GameObject wallPuzzleActivate;
    public GameObject lockedWall;

    public string questName;
    public int questProgress;

    public bool inBattle;
    public bool finishBattle;

    bool playerInRange;

    void Update()
    {
        if (playerInRange && !inBattle || finishBattle)
        {
            WallTrigger();
        }
    }

    private void WallTrigger()
    {
        if (questName == "CS1010" && GameManager.instance.cs1010Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "CS1231" && GameManager.instance.cs1231Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "CS2030" && GameManager.instance.cs2030Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "CS2040" && GameManager.instance.cs2040Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "MA1511" && GameManager.instance.ma1511Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "MA1512" && GameManager.instance.ma1512Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "MA1508E" && GameManager.instance.ma1508EProgress < questProgress)
        {
            ActivatePuzzle();
        }
    }

    private void ActivatePuzzle()
    {
        lockedWall.SetActive(true);
        wallPuzzleActivate.SetActive(true);
        finishBattle = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
