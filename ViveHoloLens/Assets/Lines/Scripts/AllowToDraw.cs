// ------------------------------------
// SCRIPT      : AllowToDraw.cs
// CREATE DATE : 18.11.2017
// PURPOSE     : Check if the player is allowed to draw
// 
// AUTHOR      : GABRIEL Michel I-3
// ------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowToDraw : MonoBehaviour
{
    [HideInInspector]
    public bool CanDraw { get; set; }

    [HideInInspector]
    public bool PlayerTurn { get; set; }

    [HideInInspector]
    public bool HasDrawn { get; set; }

    private SteamVR_TrackedObject trackedObj;

    public string player;

    public bool HoloLensPlayer;

    // -- Get the reference to the other controller
    public SteamVR_TrackedController otherPlayerController;
    private AllowToDraw otherPlayer;
    private TicTacToeCtrl grid;

    private void Start()
    {
        otherPlayer = otherPlayerController.GetComponent<AllowToDraw>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        PlayerTurn = true;
    }

    /// <summary>
    /// This method detect when a n other gameobject enter in the collision area.
    /// </summary>
    /// <param name="other">Collided GameObject</param>
    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObj = other.gameObject;
        if (collidedObj.tag == "TicTacToeCell")
        {
            if (!grid)
            {
                grid = GameObject.FindGameObjectWithTag("TicTacToe").GetComponent<TicTacToeCtrl>();
            }
            else
            {
                if (grid.IsGameOver())
                {
                    CanDraw = false;
                    return;
                }
            }
            CanDraw = collidedObj.GetComponent<TicTacToeCell>().CellValue == 0;
            if (CanDraw && PlayerTurn)
                LongControllerVibration(500, 1);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="length">How long the vibration should go for</param>
    /// <param name="strength">Vibration strength from 0-1</param>
    /// <returns></returns>
    private void LongControllerVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
        }
    }

    /// <summary>
    /// This method detect when an other gameobject exit the collision area.
    /// </summary>
    /// <param name="other">Collided GameObject</param>
    void OnTriggerExit(Collider other)
    {
        if (HasDrawn)
        {
            PlayerTurn = false;
            otherPlayer.PlayerTurn = true;
            HasDrawn = false;
        }
        CanDraw = false;
    }

}
