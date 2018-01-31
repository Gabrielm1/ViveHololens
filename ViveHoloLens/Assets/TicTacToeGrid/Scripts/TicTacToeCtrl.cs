// ------------------------------------
// SCRIPT      : TicTacToeCtrl.cs
// CREATE DATE : 20.10.2017
// PURPOSE     : Detect the winner
// 
// AUTHOR      : GABRIEL Michel I-3
// ------------------------------------

using System;
using UnityEngine;
using UnityEngine.Networking;

public class TicTacToeCtrl : NetworkBehaviour
{

    public TicTacToeCell[] cells; // The table of cells numeroted form top left to bottom right.

    public delegate void PlayerWinHandler(string player);
    public event PlayerWinHandler PlayerWin;

    public bool gameOver;
    [SyncVar]
    private int scoreA;

    [SyncVar]
    private int scoreB;
    private GameObject[] scoreViews;

    [SerializeField]
    private GameObject winnerLinePrefab;

    private void Start()
    {
        GameObject eventHandler = GameObject.FindGameObjectWithTag("EventAnnouncer");
        eventHandler.GetComponent<EventAnnouncer>().SetTicTacToeCtrl(this);
        foreach (TicTacToeCell cell in cells)
        {
            cell.ValueChanged += IsMatchOver;
        }
        scoreViews = GameObject.FindGameObjectsWithTag("lblScore");
        scoreA = scoreB = 0;
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void IsMatchOver()
    {
        if (!gameOver)
        {
            //check rows
            if (Check(0, 1, 2)) return;
            if (Check(3, 4, 5)) return;
            if (Check(6, 7, 8)) return;

            //check cols
            if (Check(0, 3, 6)) return;
            if (Check(1, 4, 7)) return;
            if (Check(2, 5, 8)) return;

            // check diagonals
            if (Check(0, 4, 8)) return;
            if (Check(2, 4, 6)) return;
            if (IsEquality())
            {
                gameOver = true;
                EndOfTheGame("Equality, there is no winner");
            }
        }
    }

    private bool Check(int startCell, int middleCell, int endCell)
    {
        int val = cells[startCell].CellValue + cells[middleCell].CellValue + cells[endCell].CellValue;
        if (val == 3) //PlayerA or B wins
        {
            string message = ("playerA" == Utils.HoloPlayer) ? "You win" : "You loose";
            CmdDrawLine(startCell, endCell, message);
            scoreA++;
        }
        else if (val == -3)
        {
            string message = ("playerB" == Utils.HoloPlayer) ? "You win" : "You loose";
            CmdDrawLine(startCell, endCell, message);
            scoreB++;
        }
        return gameOver;
    }

    [Command]
    private void CmdUpdateScore()
    {
        foreach (GameObject txt in scoreViews)
        {
            txt.GetComponent<TextMesh>().text = "Score : " + scoreA + " : " + scoreB;
        }
    }

    private bool IsEquality()
    {
        foreach (TicTacToeCell cell in cells)
        {
            if (cell.CellValue == 0) return false;
        } // means each cells has been drawn, so equality
        return true;
    }

    /// <summary>
    /// This method draw the winner's line 
    /// </summary>
    /// <param name="startCell">The start cell's number (position in the grid)</param>
    /// <param name="endCell">The end cell's number (position in the grid)</param>
    [Command]
    private void CmdDrawLine(int startCell, int endCell, string winner)
    {
        Vector3 start = cells[startCell].transform.localPosition;
        Vector3 end = cells[endCell].transform.localPosition;
        GameObject winnerLine = Instantiate(winnerLinePrefab, transform);
        LineRenderer line = winnerLine.GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        gameOver = true;
        NetworkServer.Spawn(winnerLine);
        winnerLine.transform.localPosition = new Vector3(0, 0, 0);
        winnerLine.transform.localRotation = Quaternion.identity;
        AppLoc(startCell, endCell);
        EndOfTheGame(winner);
    }

    [Server]
    private void AppLoc(int startCell, int endCell)
    {
        RpcApplyLocalAlignment(startCell, endCell);
    }

    [Server]
    private void EndOfTheGame(string msg)
    {
        RpcEnOfTheGame(msg);
    }

    [ClientRpc]
    private void RpcEnOfTheGame(string msg)
    {
        CmdUpdateScore();
        if (Utils.IsVR)
        {
            PlayerWin(!(msg == "You loose") ? "Vive player looses" : "Vive player wins");

        }
        else if (Utils.IsHoloLens) // need to be aligned 
        {
            PlayerWin(msg);
        }
    }

    /// <summary>
    /// This method align the winner's line for the HoloLens client.
    /// </summary>
    /// <param name="startCell">The start cell's number (position in the grid)</param>
    /// <param name="endCell">The end cell's number (position in the grid)</param>
    [ClientRpc]
    private void RpcApplyLocalAlignment(int startCell, int endCell)
    {

        if (Utils.IsHoloLens) // need to be aligned 
        {
            GameObject wLine = GameObject.FindGameObjectWithTag("TicTacToeWinnerLine");
            LineRenderer l = wLine.GetComponent<LineRenderer>();
            Vector3 start = cells[startCell].transform.localPosition;
            Vector3 end = cells[endCell].transform.localPosition;
            wLine.transform.rotation = transform.rotation;
            wLine.transform.position = transform.position;
            l.SetPosition(0, start);
            l.SetPosition(1, end);
        }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
