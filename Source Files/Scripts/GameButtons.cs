using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class GameButtons : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Update()
    {
        if (DatabaseManager.instance.hasGameData)
        {
            continueGameButton.interactable = true;
        }
        else
        {
            continueGameButton.interactable = false;
        }
    }
    public void NewGame()
    {
        DisableMenuButtons();
        //create new game instance
        DatabaseManager.instance.DeleteUserData();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("IntroTutorial");
    }

    public void ContinueGame()
    {
        if (DataPersistenceManager.instance.gameData.endingProgress == 6)
        {
            DisableMenuButtons();
            SceneManager.LoadSceneAsync("DefendNorthForest");
        }
        else
        {
            DisableMenuButtons();
            SceneManager.LoadSceneAsync("PlayerHouse");
        }
    }

    //disable so that player cant click continue button if there is no saved data
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
