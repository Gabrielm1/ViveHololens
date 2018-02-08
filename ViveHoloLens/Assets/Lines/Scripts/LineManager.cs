// ------------------------------------
// SCRIPT      : LineManager.cs
// CREATE DATE : 18.11.2017
// PURPOSE     : 
// 
// AUTHOR      : GABRIEL Michel I-3
// ------------------------------------
using UnityEngine;
using UnityEngine.Networking;

public class LineManager : NetworkBehaviour
{
    public SteamVR_TrackedController playerController;
    public SteamVR_TrackedController otherPlayerController;

    public GameObject lineContainer;
    public GameObject linePrefabs;

    [SyncVar]
    private bool isDrawing = false;

    private GameObject currentLine = null;

    private AllowToDraw otherPlayer;
    private AllowToDraw player;

    void Start()
    {
        otherPlayer = otherPlayerController.GetComponent<AllowToDraw>();
        player = playerController.GetComponent<AllowToDraw>();
    }
    /// <summary>
    /// 
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();
        playerController.TriggerClicked += TriggerClicked;
        playerController.TriggerUnclicked += TriggerUnclicked;
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (isClient && isDrawing)
        {
            UpdateLinePosition();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [ClientCallback]
    private void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (isDrawing)
        {
            CmdFinishDrawing();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [ClientCallback]
    private void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (!isDrawing)
        {
            HideControllerMsg();
            CmdStartDrawing();
        }
    }

    private void HideControllerMsg()
    {
        if (player.CanDraw)
        {
            player.HideMessage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Command]
    private void CmdStartDrawing()
    {
        if (player.CanDraw && player.PlayerTurn)
        {
            currentLine = Instantiate(linePrefabs, lineContainer.transform);
            currentLine.GetComponent<LineController>().StartPlacing();
            currentLine.name = linePrefabs.name;
            UpdateLinePosition();
            NetworkServer.Spawn(currentLine);
            RpcSetBlockParent(currentLine);
            isDrawing = true;
            player.HasDrawn = true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="line"></param>
    [ClientRpc]
    private void RpcSetBlockParent(GameObject line)
    {
        line.transform.SetParent(lineContainer.transform, false);
    }
    /// <summary>
    /// 
    /// </summary>
    [Command]
    private void CmdFinishDrawing()
    {
        // Add the collider
        CapsuleCollider collider = currentLine.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.radius = currentLine.GetComponent<LineRenderer>().endWidth;
        currentLine = null;
        isDrawing = false;
    }

    /// <summary>
    /// 
    /// </summary>
    [ClientCallback]
    private void UpdateLinePosition()
    {
        CmdUpdateCubePosition(playerController.transform.position);
    }

    /// <summary>
    /// Add a new point to the LineRenderer. If the user exits the table cell, the CmdFinishDrawing() method is called.
    /// </summary>
    /// <param name="position">The current position of the controller</param>
    [Command]
    private void CmdUpdateCubePosition(Vector3 position)
    {
        if (!currentLine) return;
        if (player.CanDraw)
        {
            currentLine.GetComponent<LineController>().AddPoint(position);
        }
        else
        {
            CmdFinishDrawing();
        }
    }
}