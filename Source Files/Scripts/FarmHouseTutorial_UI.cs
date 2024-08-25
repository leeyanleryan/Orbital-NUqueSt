using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmHouseTutorial_UI : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    public GameObject enterPlayerHouseFirst;

    void Update()
    {
        if (GameManager.instance.tutorialProgress == 1)
        {
            tutorialText.text = "Enter your house west of the path";
        }
        else
        {
            Destroy(enterPlayerHouseFirst);
            Destroy(this.gameObject);
        }
    }
}
