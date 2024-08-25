using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenSecDialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialText.text = "Please talk to the secretary first.";
        }
    }
}
