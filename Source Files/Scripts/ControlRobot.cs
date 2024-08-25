using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlRobot : MonoBehaviour
{
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private CinemachineVirtualCamera virtualCamera;
    private RobotMovement robotMovement;
    private GameObject robot;
    public GameObject questPanel;
    public GameObject gpsPanel;
    public GameObject timerPanel;
    public float timer;
    public float givenTimer;

    public bool TimerOn;
    public CompleteCEGQuest questComplete;
    private PlayerQuests playerQuest;
    public string questName;

    void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        robot = GameObject.Find("Robot");
        robotMovement = GameObject.Find("Robot").GetComponent<RobotMovement>();
        if (questName == "CG1111A")
        {
            timer = 45f;
        }
        else if (questName == "EG1311")
        {
            timer = 30f;
        }
        else if (questName == "CG2111A")
        {
            timer = 45f;
        }
        TimerOn = false;

        playerQuest = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    void FixedUpdate()
    {
        if (TimerOn)
        {
            StartCountDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < 6; i++)
            {
                //if there is an active quest in the slot
                if (playerQuest.questList.questSlots[i].questName == questName && !playerQuest.questList.questSlots[i].done)
                {
                    playerItems.disableToolbar = true;
                    playerMovement.enabled = false;
                    OpenPanels();
                }
            }
        }
    }

    private void OpenPanels()
    {
        questPanel.SetActive(true);
    }

    public void YesPressed()
    {
        questPanel.SetActive(false);
        timerPanel.SetActive(true);
        robotMovement.enabled = true;
        if (questName != "CG2111A")
        {
            virtualCamera.Follow = robot.transform;
        }
        else
        {
            gpsPanel.SetActive(true);
        }
        TimerOn = true;
        if (questName == "CG1111A" || questName == "CG2111A")
        {
            questComplete.colorsDetected.Clear();
        }
        playerItems.disableToolbar = true;
        playerMovement.enabled = false;
        //playerMovement.movespeed = 0;
    }

    public void NoPressed()
    {
        if (gpsPanel != null)
        {
            gpsPanel.SetActive(false);
        }
        questPanel.SetActive(false);
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
        robotMovement.enabled = false;
    }

    [SerializeField]
    private void StartCountDown()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            int roundedTimer = Mathf.FloorToInt(timer);
            TextMeshProUGUI timerText = timerPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            timerText.text = roundedTimer.ToString();
        }
        else
        {
            robot.transform.position = new Vector2(-1.933f, -0.352f);
            GameObject player = GameObject.Find("Player");
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
            robotMovement.enabled = false;
            virtualCamera.Follow = player.transform;
            TimerOn = false;
            timer = givenTimer;
            if (gpsPanel != null)
            {
                gpsPanel.SetActive(false);
            }
            if (GameObject.Find("ColoredTiles") != null)
            {
                GameObject.Find("YellowTile").GetComponent<SpriteRenderer>().sprite = null;
                GameObject.Find("YellowTile").GetComponent<ColorDetectionCG1111>().hasVisited = false;
                GameObject.Find("RedTile").GetComponent<SpriteRenderer>().sprite = null;
                GameObject.Find("RedTile").GetComponent<ColorDetectionCG1111>().hasVisited = false;
                GameObject.Find("BlueTile").GetComponent<SpriteRenderer>().sprite = null;
                GameObject.Find("BlueTile").GetComponent<ColorDetectionCG1111>().hasVisited = false;
                GameObject.Find("PurpleTile").GetComponent<SpriteRenderer>().sprite = null;
                GameObject.Find("PurpleTile").GetComponent<ColorDetectionCG1111>().hasVisited = false;
            }
            if (GameObject.Find("ShootingBoard") != null)
            {
                GameObject.Find("ShootingBoard").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Quest/notdoneBoard");
            }
            questComplete.colorsDetected.Clear();
            questComplete.shotLanded = false;
            timerPanel.SetActive(false);
            //playerMovement.movespeed = 1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
        questPanel.SetActive(false);
        //playerMovement.movespeed = 1f;
    }
}
