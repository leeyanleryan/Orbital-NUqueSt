using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangingLayerForTorch : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer torchRenderer;

    void Start()
    {
        torchRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            torchRenderer.sortingOrder = 5;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            torchRenderer.sortingOrder = 15;
        }
    }
}
