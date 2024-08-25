using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueenChecker : MonoBehaviour
{
    public List<Vector2Int> queenPositions = new List<Vector2Int>();

    [SerializeField] private List<Vector2Int> seenBeforeList = new List<Vector2Int>();
    private Dictionary<Vector2Int, int> seenBeforeDict = new Dictionary<Vector2Int, int>();

    private PlayerQuests playerQuests;

    public static QueenChecker instance;

    private void Start()
    {
        instance = this;
        seenBeforeList = GameManager.instance.cs2040SeenBefore;
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        foreach (Vector2Int seenBefore in seenBeforeList)
        {
            if (!seenBeforeDict.ContainsKey(seenBefore))
            {
                seenBeforeDict.Add(seenBefore, 1);
            }
        }
    }

    public bool CheckX()
    {
        Dictionary<int, int> dictX = new Dictionary<int, int>();
        for (int i = 0; i < queenPositions.Count; i++)
        {
            if (dictX.ContainsKey(queenPositions[i].x))
            {
                return true;
            }
            else
            {
                dictX.Add(queenPositions[i].x, 1);
            }
        }
        return false;
    }

    public bool CheckDiag(Vector2Int queenPosition)
    {
        int tempY = queenPosition.y + 1;
        for (int x = queenPosition.x - 1; x > -1; x--)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY++;
            }
        }
        tempY = queenPosition.y + 1;
        for (int x = queenPosition.x + 1; x < 6; x++)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY++;
            }
        }
        tempY = queenPosition.y - 1;
        for (int x = queenPosition.x - 1; x > -1; x--)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY--;
            }
        }
        tempY = queenPosition.y - 1;
        for (int x = queenPosition.x + 1; x < 6; x++)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY--;
            }
        }
        return false;
    }

    public bool HasSeenBefore()
    {
        int seenIntX = 0;
        int seenIntY = 0;
        for (int i = 5; i > -1; i--)
        {
            seenIntX += (queenPositions[5 - i].x + 1) * (int)Math.Pow(10, i);
            seenIntY += (queenPositions[5 - i].y + 1) * (int)Math.Pow(10, i);
        }
        Vector2Int seenPos = new Vector2Int(seenIntX, seenIntY);
        if (seenBeforeDict.ContainsKey(seenPos))
        {
            return true;
        }
        playerQuests.cs2040SeenBefore.Add(seenPos);
        seenBeforeList.Add(seenPos);
        seenBeforeDict.Add(seenPos, 1);
        return false;
    }
}
