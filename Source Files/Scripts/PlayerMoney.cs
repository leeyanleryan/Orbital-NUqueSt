using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI moneyText;
    public int money;

    private PlayerTutorial playerTutorial;

    public PlayerPositionSO startingPosition;

    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        playerTutorial = gameObject.GetComponent<PlayerTutorial>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            money = GameManager.instance.money;
        }
    }

    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerQuests>().endingProgress == 1
            || GameObject.Find("Player").GetComponent<PlayerQuests>().endingProgress == 2
            || GameObject.Find("Player").GetComponent<PlayerQuests>().endingProgress == 5
            || GameObject.Find("Player").GetComponent<PlayerQuests>().endingProgress == 6)
        {
            moneyText.text = "";
            return;
        }
        else if (playerTutorial.tutorialProgress >= 3)
        {
            GameManager.instance.money = money;
            moneyText.text = "GPA: " + money;
        }
        else
        {
            moneyText.text = "";
        }
    }

    public void LoadData(GameData data)
    {
        money = data.money;
    }

    public void SaveData(GameData data)
    {
        data.money = money;
    }
}
