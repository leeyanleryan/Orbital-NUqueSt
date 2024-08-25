using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailDefend_UI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject button;

    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

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
            ContinueButton();
        }
    }

    public void ContinueButton()
    {
        background.SetActive(false);
        text.SetActive(false);
        button.SetActive(false);
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
    }
}
