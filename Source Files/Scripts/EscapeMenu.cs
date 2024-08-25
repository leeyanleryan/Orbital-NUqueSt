using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public Button saveButton;
    public Button closeButton;
    public Button settingButton;
    public GameObject escapeMenuPanel;
    //  public GameObject settingMenuPanel;

    private void Start()
    {
       // escapeMenuPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape buttonn is pressed");
            escapeMenuPanel.SetActive(true);
        }    
    }

    public void closeEscape()
    {
        escapeMenuPanel.SetActive(false);
    }
    

}
