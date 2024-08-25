using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetectionCG1111 : MonoBehaviour
{
    public CompleteCEGQuest questComplete;
    public string colorName;
    public bool hasVisited;

    void Start()
    {
        hasVisited = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("robot"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Quest/villagePavement");
            if (!hasVisited)
            {
                questComplete.colorsDetected.Add(colorName);
                hasVisited = true;
            }
        }
    }
}
