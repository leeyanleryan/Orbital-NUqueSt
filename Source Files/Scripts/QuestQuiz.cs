using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQuiz : MonoBehaviour
{
    private GameObject questionPanel;
    private GameObject correctAnsPanel;
    private GameObject wrongAnsPanel;

    public PlayerQuests playerQuest;
    // Start is called before the first frame update
    void Start()
    {
        questionPanel = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        correctAnsPanel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        wrongAnsPanel = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        playerQuest = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CorrectAnswer()
    {
        questionPanel.SetActive(false);
        correctAnsPanel.SetActive(true);
        wrongAnsPanel.SetActive(false);
        for (int i = 0; i < 6; i += 1)
        {
            if (playerQuest.questList.questSlots[i].questName == "PC1201")
            {
                playerQuest.questList.questSlots[i].done = true;    
            }
        }
    }

    public void WrongAnswer()
    {
        questionPanel.SetActive(false);
        wrongAnsPanel.SetActive(true);
        correctAnsPanel.SetActive(false);
    }

    public void OpenQuestionPanel()
    {
        questionPanel.SetActive(true);
        correctAnsPanel.SetActive(false);
        wrongAnsPanel.SetActive(false);
    }

    public void CloseAllPanels()
    {
        questionPanel.SetActive(false);
        correctAnsPanel.SetActive(false);
        wrongAnsPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < 6; i += 1)
        {
            if (collision.CompareTag("Player") && playerQuest.questList.questSlots[i].questName == "PC1201" && playerQuest.questList.questSlots[i].done == false)
            {
                OpenQuestionPanel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CloseAllPanels();
        }
    }
}
