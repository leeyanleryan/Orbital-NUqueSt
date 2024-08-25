using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);

    //ref to save because we are changing the file and not reading it
    void SaveData(GameData data);
}
