using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using PlayFab;

public class DatabaseManager : MonoBehaviour
{
    public string userID;
    public static DatabaseManager instance { get; private set; }
    public GameData databaseGameData;

    public bool hasGameData;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DatabaseManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void CreateUser(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {userID, json}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    
    public void LoadGameData()
    {
       PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharactersDataRecieved, OnLoadGameDataError);    
    }

    public void DeleteUserData()
    {

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {userID, ""}
            }
        }, response =>
        {
            Debug.Log("Successfully deleted user data");
        }, OnDeleteError);
    }
    public void OnDeleteError(PlayFabError error)
    {
        Debug.Log("Error deleting user data");
        print(error.ErrorMessage);
    }

    public void OnDataSend(UpdateUserDataResult result)
    {
    }
    public void OnCharactersDataRecieved(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey(userID))
        {
            databaseGameData = JsonUtility.FromJson<GameData>(result.Data[userID].Value);
            DataPersistenceManager.instance.gameData = databaseGameData;
            hasGameData = true;
        }
        else
        {
            Debug.LogWarning("User data not found - at DatabaseManager.cs");
            hasGameData = false;
        }
    }
    void OnError(PlayFabError error)
    {
        print(error.ErrorMessage);
    }

    void OnLoadGameDataError(PlayFabError error)
    {
        Debug.LogError("Error loading game data: " + error.ErrorMessage);
    }

}