using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToChangeName : MonoBehaviour
{
    private DialogueManager dialogueManager;
    public string gameObjectNPCName;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueManager.localNPCName = gameObjectNPCName;
    }
}
