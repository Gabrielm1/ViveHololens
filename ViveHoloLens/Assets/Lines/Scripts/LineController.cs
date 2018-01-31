// ------------------------------------
// SCRIPT      : LineController.cs
// CREATE DATE : 18.11.2017
// PURPOSE     : 
// 
// AUTHOR      : GABRIEL Michel I-3
// ------------------------------------
using UnityEngine;
using UnityEngine.Networking;

public class LineController : NetworkBehaviour
{
    private int numClick = 0;

    private double epsilon = 1.9E-02;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    [ClientRpc] // Serveur au client
    void RpcAddPointToLine(Vector3 position)
    {
        LineRenderer line = this.GetComponent<LineRenderer>();
        if (numClick == 0)
        {
            line.numPositions = numClick + 1;
            line.SetPosition(numClick++, position);
        }
        else
        {
            Vector3 oldPos = line.GetPosition(numClick - 1);
            if (Vector3.Distance(oldPos, position) > epsilon)
            {
                line.numPositions = numClick + 1;
                line.SetPosition(numClick++, position);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    [ServerCallback]
    public void AddPoint(Vector3 position)
    {
        RpcAddPointToLine(position);
    }
    /// <summary>
    /// 
    /// </summary>
    [ServerCallback]  // Serveur
    public void StartPlacing()
    {
        numClick = 0;
    }
}