using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageTutorial_UI : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    public GameObject TransitionColliders;

    private void Update()
    {
        if (GameManager.instance.tutorialProgress == 1)
        {
            tutorialText.text = "Head south of the village to see your house";
            TransitionColliders.SetActive(false);
        }
        else
        {
            GameObject.Find("WestPathBlock").SetActive(false);
            GameObject.Find("NorthPathBlock").SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
