using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTK1234Quest : MonoBehaviour
{
    private PlayerQuests playerQuests;

    bool check1;
    bool check2;
    bool check3;

    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    void Update()
    {
        if (!check1 && GameObject.Find("LeatherPiece") == null)
        {
            playerQuests.dtk1234Collected[1] = 1;
            check1 = true;
        }
        if (!check2 && GameObject.Find("LeatherPiece (1)") == null)
        {
            playerQuests.dtk1234Collected[2] = 1;
            check2 = true;
        }
        if (!check3 && GameObject.Find("LeatherPiece (2)") == null)
        {
            playerQuests.dtk1234Collected[3] = 1;
            check3 = true;
        }
    }

    public void ChangeActive(List<int> list)
    {
        if (list[0] == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (list[1] == 1)
            {
                GameObject.Find("LeatherPiece").SetActive(false);
            }
            if (list[2] == 1)
            {
                GameObject.Find("LeatherPiece (1)").SetActive(false);
            }
            if (list[3] == 1)
            {
                GameObject.Find("LeatherPiece (2)").SetActive(false);
            }
        }
    }
}
