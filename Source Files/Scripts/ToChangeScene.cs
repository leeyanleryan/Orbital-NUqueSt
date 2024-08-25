using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToChangeScene : MonoBehaviour
{
    public int indexOfScene;
    public Vector3 playerPosition;
    public PlayerPositionSO storePlayerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            storePlayerPosition.InitialValue = playerPosition;
            storePlayerPosition.transittedScene = true;
            DataPersistenceManager.instance.sceneTransitted = true;
            SceneManager.LoadScene(indexOfScene, LoadSceneMode.Single);          
        }
    }
}
