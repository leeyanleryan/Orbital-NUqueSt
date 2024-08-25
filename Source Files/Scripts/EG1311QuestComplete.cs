using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EG1311QuestComplete : MonoBehaviour
{
    public CompleteCEGQuest completeCEGQuest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eg1311Bullet"))
        {
            completeCEGQuest.shotLanded = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Quest/doneBoard");
        }
    }
}
