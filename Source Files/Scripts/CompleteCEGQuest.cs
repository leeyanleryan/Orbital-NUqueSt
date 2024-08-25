using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteCEGQuest : MonoBehaviour
{
    private PlayerItems playerItems;
    private PlayerQuests playerQuest;
    private PlayerMovement playerMovement;
    private CinemachineVirtualCamera virtualCamera;
    private RobotMovement robotMovement;
    private GameObject player;
    private bool allColorsDetected;
    public GameObject gpsPanel;
    public GameObject timerPanel;
    public float timer;

    public ControlRobot controlRobot;
    public bool shotLanded; 

    public List<string> colorsDetected;

    void Start()
    {
        shotLanded = false;
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerQuest = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        robotMovement = GameObject.Find("Robot").GetComponent<RobotMovement>();
        player = GameObject.Find("Player");

        allColorsDetected = false;
        colorsDetected = new List<string>();

    }

    public virtual void Update()
    {
        if (colorsDetected.Count == 4)
        {
            allColorsDetected = true;
        }
        else
        {
            allColorsDetected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("robot"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (playerQuest.questList.questSlots[i].questName == "CG1111A" && allColorsDetected)
                {
                    controlRobot.TimerOn = false;
                    timer = 60f;
                    TextMeshProUGUI timerText = timerPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                    timerText.text = timer.ToString();
                    playerQuest.questList.questSlots[i].done = true;
                    playerItems.disableToolbar = false;
                    playerMovement.enabled = true;
                    robotMovement.enabled = false;
                    virtualCamera.Follow = player.transform;
                    timerPanel.SetActive(false);
                    Quest_UI quest_UI = GameObject.Find("Quest").GetComponent<Quest_UI>();
                    quest_UI.questSlots[i].questStatus.SetActive(true);
                    //playerMovement.movespeed = 1f;
                }
                else if (playerQuest.questList.questSlots[i].questName == "EG1311" && shotLanded)
                {
                    controlRobot.TimerOn = false;
                    timer = 30f;
                    TextMeshProUGUI timerText = timerPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                    timerText.text = timer.ToString();
                    playerQuest.questList.questSlots[i].done = true;
                    playerItems.disableToolbar = false;
                    playerMovement.enabled = true;
                    robotMovement.enabled = false;
                    virtualCamera.Follow = player.transform;
                    timerPanel.SetActive(false);
                    Quest_UI quest_UI = GameObject.Find("Quest").GetComponent<Quest_UI>();
                    quest_UI.questSlots[i].questStatus.SetActive(true);
                    //playerMovement.movespeed = 1f;
                }
                else if (playerQuest.questList.questSlots[i].questName == "CG2111A" && allColorsDetected)
                {
                    controlRobot.TimerOn = false;
                    timer = 45f;
                    TextMeshProUGUI timerText = timerPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                    timerText.text = timer.ToString();
                    playerQuest.questList.questSlots[i].done = true;
                    playerItems.disableToolbar = false;
                    playerMovement.enabled = true;
                    robotMovement.enabled = false;
                    gpsPanel.SetActive(false);
                    timerPanel.SetActive(false);
                    Quest_UI quest_UI = GameObject.Find("Quest").GetComponent<Quest_UI>();
                    quest_UI.questSlots[i].questStatus.SetActive(true);
                    //playerMovement.movespeed = 1f;
                }
            }
        }
    }
}
