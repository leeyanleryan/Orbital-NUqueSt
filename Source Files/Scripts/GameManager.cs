using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;
    public static GameManager instance;
    public int level;
    public float exp;

    public QuestList questList;

    public Inventory inventory;
    public Inventory toolbar;

    public Inventory chest0; // chest0: PlayerHouse
    public Inventory chest1; // chest1: UNUSED
    public Inventory chest2; // chest2: Cave_1
    public Inventory chest3; // chest3: Village_WeaponShop
    public Inventory chest4; // chest4: ArtistHouse
    public Inventory chest5; // chest5: ScientistHouse
    public Inventory chest6; // chest6: ScientistHouse
    public Inventory chest7; // chest7: GeologistHouse
    public Inventory chest8; // chest8: NerdNPC House
    public List<Inventory> chestList = new List<Inventory>();
    public bool hasAddedToChest;

    public Inventory shop0; // shop0: blacksmith
    public Inventory shop1; // shop1: generalshop
    public List<Inventory> shopList = new List<Inventory>();

    public int money;

    // Used in PlayerTutorial. The values will go from 0 to 3. At 3, the tutorial UI no longer appears in any scene.
    public int tutorialProgress;

    // Used in PlayerQuest. If a quest has begun, the progress changes from -1 to 0, and certain GameObjects in the scene change.
    // Some quests can range up to an int value of 7 while others 1.
    public int cs1010Progress;
    public int cs1231Progress;
    public int cs2030Progress;
    public int cs2040Progress;
    public List<Vector2Int> cs2040SeenBefore = new List<Vector2Int>();

    public int ma1511Progress;
    public int ma1512Progress;
    public int ma1508EProgress;

    public int eg1311Progress;
    public int cg2111aProgress;

    public List<int> dtk1234Collected = new List<int>() { 0, 0, 0, 0 };

    public List<string> completedQuestNames = new List<string>();
    public List<string> completedQuestDescs = new List<string>();

    public List<string> questScrollNames = new List<string>();
    public List<int> questScrollInserted = new List<int>() { 0, 0, 0, 0, 0, 0 };

    public int endingProgress;

    public float rockDayChecker;
    public List<string> listOfRockSceneNames = new List<string>();
    public List<List<string>> listOfRockNames = new List<List<string>>();
    public List<List<int>> listOfRockStates = new List<List<int>>();

    public float enemyDayChecker;
    public List<string> listOfEnemySceneNames = new List<string>();
    public List<List<string>> listOfEnemyNames = new List<List<string>>();
    public List<List<int>> listOfEnemyStates = new List<List<int>>();

    // for day and night system
    public float hours;
    public float minutes;
    public float seconds;
    public float day;

    public string story;

    public List<Vector3Int> seedPositions = new List<Vector3Int>();
    public List<string> seedNames = new List<string>();
    public List<float> seedNextGrowths = new List<float>();

    public float energy = 100;

    void Awake()
    { 
        if (instance != null)
        {
            //Debug.LogError("Found more than one GameManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        instance.questList = new QuestList(6);

        instance.inventory = new Inventory("Inventory", 21);
        instance.toolbar = new Inventory("Toolbar", 7);

        instance.chest0 = new Inventory("Chest0", 21);
        instance.chest1 = new Inventory("Chest1", 21);
        instance.chest2 = new Inventory("Chest2", 21);
        instance.chest3 = new Inventory("Chest3", 21);
        instance.chest4 = new Inventory("Chest4", 21);
        instance.chest5 = new Inventory("Chest5", 21);
        instance.chest6 = new Inventory("Chest6", 21);
        instance.chest7 = new Inventory("Chest7", 21);
        instance.chest8 = new Inventory("Chest8", 21);
        chestList.Add(instance.chest0);
        chestList.Add(instance.chest1);
        chestList.Add(instance.chest2);
        chestList.Add(instance.chest3);
        chestList.Add(instance.chest4);
        chestList.Add(instance.chest5);
        chestList.Add(instance.chest6);
        chestList.Add(instance.chest7);
        chestList.Add(instance.chest8);

        instance.shop0 = new Inventory("Shop0", 21);
        instance.shop1 = new Inventory("Shop1", 21);
        shopList.Add(instance.shop0);
        shopList.Add(instance.shop1);

        DontDestroyOnLoad(gameObject);  //else
        energy = 100;
    }
}
