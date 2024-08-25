using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerPositionSO : ScriptableObject
{
    public Vector3 InitialValue;
    public bool transittedScene = false;
    public bool playerDead = false;
}
