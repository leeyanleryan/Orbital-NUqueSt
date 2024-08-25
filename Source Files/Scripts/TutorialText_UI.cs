using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText_UI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;

    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private int curr;
    private bool done;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerItems.disableToolbar = true;
        playerMovement.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curr++;
        }
        if (curr == 0)
        {
            text.text = "You were running for your life after being invaded by monsters.";
        }
        else if (curr == 1)
        {
            text.text = "After running for a while, you reach a dead end.";
        }
        else if (!done)
        {
            background.SetActive(false);
            text.gameObject.SetActive(false);
            button.SetActive(false);
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
            done = true;
        }
    }

    public void ContinueButton()
    {
        curr++;
    }
}
