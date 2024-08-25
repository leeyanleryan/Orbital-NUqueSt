using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerFainted : MonoBehaviour
{
    public GameObject globalVolume;
    private ClockManager clockManager;
    public Animator transition;
    public Animator playerAnimation;
    GameObject originalGameObject;
    private float volumeHealthSlider;

    private GameObject playerHitBox;
    private Health healthScript;
    public PlayerPositionSO playerPositionSO;

    private PlayerMovement playerMovement;
    public PlayerEnergy playerEnergy;

    public bool isSleeping;
    
    // Start is called before the first frame update
    void Start()
    {
        globalVolume = GameObject.Find("Global Volume");
        clockManager = globalVolume.GetComponent<ClockManager>();
        originalGameObject = GameObject.Find("HealthBar");
        volumeHealthSlider = originalGameObject.GetComponent<Slider>().value;
        playerAnimation = GameObject.Find("Player").GetComponent<Animator>();
        playerHitBox = GameObject.Find("PlayerHitBox");
        healthScript = playerHitBox.GetComponent<Health>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerEnergy = GameObject.Find("Player").GetComponent<PlayerEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        volumeHealthSlider = originalGameObject.GetComponent<Slider>().value;
        if (!isSleeping && (clockManager.hours > 23 || volumeHealthSlider <= 0))
        {
            if (SceneManager.GetActiveScene().name == "PlayerHouse")
            {
                GameObject.Find("Bed").GetComponent<BedSleep>().Message.SetActive(false);
            }
            GoBackHome();
        }
    }

    public void GoBackHome()
    {
        StartCoroutine(WaitAnimation());
    }

    IEnumerator WaitAnimation()
    {
        playerAnimation.SetTrigger("Fainted");
        playerMovement.enabled = false;

        yield return new WaitForSeconds(2);

        transition.SetTrigger("Fainted");

        yield return new WaitForSeconds(2);
        GameManager.instance.day += 1;
        clockManager.days += 1;
        if (SceneManager.GetActiveScene().name == "DefendVillage")
        {
            PlayerItems playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
            for (int i = 0; i < playerItems.inventory.slots.Count; i++)
            {
                if (playerItems.inventory.slots[i].itemName != null)
                {
                    playerItems.inventory.Remove(i, playerItems.inventory.slots[i].count);
                }
            }
            for (int i = 0; i < playerItems.toolbar.slots.Count; i++)
            {
                if (playerItems.toolbar.slots[i].itemName != null)
                {
                    playerItems.toolbar.Remove(i, playerItems.toolbar.slots[i].count);
                }
            }
            playerItems.inventory.slots[0].AddItem(ItemManager.instance.GetItemByName("CPU Piece"));
            healthScript.health = 100;
            GameManager.instance.health = 100;
            DataPersistenceManager.instance.gameData.endingProgress = 6;
        }
        else
        {
            if (volumeHealthSlider <= 0)
            {
                healthScript.health = 50;
                GameManager.instance.health = 50;
                playerEnergy.energy = 50;
                GameManager.instance.energy = 50;
            }
            else
            {
                healthScript.health /= 2;
                GameManager.instance.health /= 2;
                playerEnergy.energy = 50;
                GameManager.instance.energy = 50;
            }
        }
        DataPersistenceManager.instance.SaveGame();
        playerPositionSO.playerDead = true;
        playerPositionSO.InitialValue = new Vector2((float)-0.072, (float)-0.264);

        if (SceneManager.GetActiveScene().name == "DefendVillage")
        {
            SceneManager.LoadScene("DefendNorthForest", LoadSceneMode.Single);
        }
        else if (SceneManager.GetActiveScene().name == "PlayerHouse")
        {
            SceneManager.LoadScene("PlayerHouse", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("FarmHouse", LoadSceneMode.Single);
        }
    }
}
