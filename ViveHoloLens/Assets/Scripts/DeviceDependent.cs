// ------------------------------------
// SCRIPT      : DeviceDependent.cs
// CREATE DATE : 20.01.2017
// PURPOSE     : Hide the 
// 
// AUTHOR       : Drew Gottlieb
// ------------------------------------
using UnityEngine;

public class DeviceDependent : MonoBehaviour
{
    public Utils.PlayerType requiredPlayerType;

    void Awake()
    {
        gameObject.SetActive(Utils.CurrentPlayerType == requiredPlayerType);
    }
}
