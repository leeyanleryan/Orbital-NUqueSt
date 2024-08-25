using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ChangingLayerOrder : MonoBehaviour
{
    [SerializeField]
    private GameObject topBush;
    TilemapRenderer topBushRenderer;
    // Start is called before the first frame update
    void Start()
    {
        topBush = gameObject.transform.GetChild(0).gameObject;
        topBushRenderer = topBush.GetComponent<TilemapRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        topBushRenderer.sortingOrder = 5;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        topBushRenderer.sortingOrder = 7;
    }
}
