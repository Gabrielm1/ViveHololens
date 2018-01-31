// ------------------------------------
// SCRIPT      : TicTacToeCell.cs
// CREATE DATE : 20.10.2017
// PURPOSE     : 
// 
// AUTHOR      : GABRIEL Michel I-3
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeCell : MonoBehaviour
{

    public delegate void ValueChangedHandler();
    public event ValueChangedHandler ValueChanged;

    public int position;  // The cell's position in the grid

    [HideInInspector]
    public int CellValue { get; set; }// The cell's value

    void OnTriggerEnter(Collider other)
    {
        // +1 for PalyerA  -1 for PlayerB
        if (CellValue == 0)
        {
            string player = other.gameObject.name;
            if (player.Equals("playerALine"))
            {
                CellValue = 1;
            }
            else if (player.Equals("playerBLine"))
            {
                CellValue = -1;
            }
            ValueChanged();
        }
    }
}
