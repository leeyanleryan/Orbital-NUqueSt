using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

    public static BGMManager instance;

    // In update, when the Play() method of AudioSource is called, it will keep playing the start of the song.
    // The bools below make sure the method is called only once.
    private List<bool> isPlayingList = new List<bool>() { false, false, false, false, false, false, false };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        bool isInside = (sceneName == "GeneralShop" || sceneName == "PlayerHouse" || sceneName == "Village_WeaponShop"
            || sceneName == "GeologistHouse" || sceneName == "ScientistHouse" || sceneName == "TownMayorHouse"
            || sceneName == "NerdNPC House" || sceneName == "TownCentre" || sceneName == "ArtistHouse" || sceneName == "BusinessHouse"
            || sceneName == "DefendTownCentre");
        bool inCave = (sceneName == "Cave_1" || sceneName == "Cave_1a" || sceneName == "Cave_2a" || sceneName == "Cave_3a"
            || sceneName == "Cave_4a" || sceneName == "Cave_5a" || sceneName == "Cave_1b" || sceneName == "DCave_1" 
            || sceneName == "DCave_1a" || sceneName == "DCave_2a");
        if (sceneName == "SampleScene 1")
        {
            ChangeAudio(0);
        }
        else if (sceneName == "Arena")
        {
            PlayerQuests playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
            if (playerQuests.endingProgress == 0)
            {
                ChangeAudio(4);
            }
            else
            {
                ChangeAudio(3);
            }
        }
        else if (sceneName == "DefendVillage")
        {
            PlayerQuests playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
            if (playerQuests.endingProgress == 3 || playerQuests.endingProgress == 4)
            {
                ChangeAudio(5);
            }
            else
            {
                ChangeAudio(3);
            }
        }
        else if (sceneName == "EndingCredits")
        {
            ChangeAudio(6);
        }
        else if (!isInside && !inCave)
        {
            ChangeAudio(3);
        }
        else if (inCave)
        {
            ChangeAudio(2);
        }
        else if (isInside)
        {
            ChangeAudio(1);
        }
    } 

    private void ChangeAudio(int index)
    {
        audioSource.clip = audioClips[index];
        if (!isPlayingList[index])
        {
            audioSource.Play();
            MakeBoolTrue(index);
        }
    }

    private void MakeBoolTrue(int index)
    {
        for (int i = 0; i < isPlayingList.Count; i++)
        {
            if (i == index)
            {
                isPlayingList[i] = true;
            }
            else
            {
                isPlayingList[i] = false;
            }
        }
    }
}
