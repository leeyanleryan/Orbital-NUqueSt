using Ink.Parsed;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UIElements;

[System.Serializable]

public class GameData
{

    public Vector3 playerPosition;
    public string name;
    public float maxHealth;
    public float health;

    public QuestList questList;

    public Inventory inventory;
    public Inventory toolbar;

    public Inventory chest0; // chest0: PlayerHouse
    public Inventory chest1; // chest1: Desert
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

    public string story;                        // quest progress for WeaponSmith NPC
    public string placeHolderStory;

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

    public List<int> dtk1234Collected = new List<int>() {0, 0, 0, 0};

    public List<string> completedQuestNames = new List<string>();
    public List<string> completedQuestDescs = new List<string>();

    public List<string> questScrollNames = new List<string>();
    public List<int> questScrollInserted = new List<int>() { 0, 0, 0, 0, 0, 0 };

    public int endingProgress;

    public float hours;

    public float day;

    public List<Vector3Int> seedPositions = new List<Vector3Int>();
    public List<string> seedNames = new List<string>();
    public List<float> seedNextGrowths = new List<float>();

    //default value;
    public GameData()
    {
        playerPosition = Vector3.zero;
       // gameScene = 3;
        maxHealth = 100;
        health = 100;

        questList = new QuestList(6);

        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);

        chest0 = new Inventory("Chest0", 21);
        chest1 = new Inventory("Chest1", 21);
        chest2 = new Inventory("Chest2", 21);
        chest3 = new Inventory("Chest3", 21);
        chest4 = new Inventory("Chest4", 21);
        chest5 = new Inventory("Chest5", 21);
        chest6 = new Inventory("Chest6", 21);
        chest7 = new Inventory("Chest7", 21);
        chest8 = new Inventory("Chest8", 21);
        chestList.Add(chest0);
        chestList.Add(chest1);
        chestList.Add(chest2);
        chestList.Add(chest3);
        chestList.Add(chest4);
        chestList.Add(chest5);
        chestList.Add(chest6);
        chestList.Add(chest7);
        chestList.Add(chest8);
        hasAddedToChest = false;

        shop0 = new Inventory("Shop0", 21);
        shop1 = new Inventory("Shop1", 21);
        shopList.Add(shop0);
        shopList.Add(shop1);

        hours = 8;

        story = "";
        placeHolderStory = "";

        cs1010Progress = -1;
        cs1231Progress = -1;
        cs2030Progress = -1;
        cs2040Progress = -1;

        ma1511Progress = -1;
        ma1512Progress = -1;
        ma1508EProgress = -1;

        eg1311Progress = -1;
        cg2111aProgress = -1;

        tutorialProgress = 0;

        day = 0;
    }
}
