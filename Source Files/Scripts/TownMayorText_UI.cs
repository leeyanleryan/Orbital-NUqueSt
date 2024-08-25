using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownMayorText_UI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;

    [SerializeField] private GameObject dialoguePanel;

    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private int curr;
    private bool done;
    private bool done2;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    private void Update()
    {
        if (GameManager.instance.tutorialProgress >= 1 && !dialoguePanel.activeSelf)
        {
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
            gameObject.SetActive(false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                curr++;
            }
            if (curr == 0 && !done2)
            {
                text.text = "You feel a strange warmth on your back, as if you were sleeping on a bed.";
                playerItems.disableToolbar = true;
                playerMovement.enabled = false;
                done2 = true;
            }
            else if (curr == 1)
            {
                text.text = "Your head hurts. You don't remember anything.";
            }
            else if (curr == 2)
            {
                text.text = "You get up anyway.";
            }
            else if (curr > 2 && !done)
            {
                background.SetActive(false);
                text.gameObject.SetActive(false);
                button.SetActive(false);
                playerItems.disableToolbar = false;
                playerMovement.enabled = true;
                done = true;
            }
        }
    }

    public void ContinueButton()
    {
        curr++;
    }
}
