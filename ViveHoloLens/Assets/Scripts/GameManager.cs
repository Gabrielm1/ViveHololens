// ------------------------------------
// SCRIPT      : GameManager.cs
// CREATE DATE : 07.01.2018
// PURPOSE     : This script allow the users to restart the game.
// 
// AUTHOR      : GABRIEL Michel I-3
// ------------------------------------

using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

    public SteamVR_TrackedController playerAController;
    public SteamVR_TrackedController playerBController;

    public GameObject lineA;
    public GameObject lineB;

    // Use this for initialization
    void Start()
    {
        playerAController.MenuButtonClicked += MenuClicked;
        playerBController.MenuButtonClicked += MenuClicked;
    }

    [ClientCallback]
    private void MenuClicked(object sender, ClickedEventArgs e)
    {
        if (AreBothMenuButtonPressed())
        {
            foreach (Transform child in lineA.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in lineB.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject wLine = GameObject.FindGameObjectWithTag("TicTacToeWinnerLine");
            if (wLine)
            {
                GameObject.Destroy(wLine);
            }
            GameObject ticTacToeGrid = GameObject.FindGameObjectWithTag("TicTacToe");
            if (ticTacToeGrid)
            {
                TicTacToeCtrl controller = ticTacToeGrid.GetComponent<TicTacToeCtrl>();
                TicTacToeCell[] cells = controller.cells;
                foreach (TicTacToeCell cell in cells)
                {
                    cell.CellValue = 0;
                }
                controller.gameOver = false;
            }
            playerAController.GetComponent<AllowToDraw>().HideArrow();
            playerBController.GetComponent<AllowToDraw>().HideArrow();

            playerAController.GetComponent<AllowToDraw>().PlayerTurn = true;
            playerBController.GetComponent<AllowToDraw>().PlayerTurn = true;

        }
    }

    [Client]
    private bool AreBothMenuButtonPressed()
    {
        return playerAController.menuPressed && playerBController.menuPressed;
    }

}
