// ------------------------------------
// SCRIPT      : AlignmentBehavior.cs
// CREATE DATE : 20.01.2017
// PURPOSE     : Hide GameObjects that have the tag hideDuringAlignment set to true.
// 
// AUTHOR      : Drew Gottlieb
// ------------------------------------
using UnityEngine;

public class AlignmentBehavior : MonoBehaviour
{
    public AlignmentManager alignmentManager;
    public bool hideDuringAlignment = false; // Must be hidden during the alignment process.
    private bool wasActive = false; // If the GameObject was active before the alignment process.

    void Start()
    {
        // Create two event. 
        alignmentManager.EventAlignmentStarted += AlignmentManager_EventAlignmentStarted;
        alignmentManager.EventAlignmentFinished += AlignmentManager_EventAlignmentFinished;
    }
    /// <summary>
    /// This method is called when the alignment begins.
    /// Hide all GameObjects that have the tag hideDuringAlignment set to true.
    /// </summary>
    private void AlignmentManager_EventAlignmentStarted()
    {
        if (hideDuringAlignment)
        {
            wasActive = this.gameObject.activeSelf;
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// This method is called when the alignment ends.
    /// Show all the GameObjects that was hidden during the alignment.
    /// </summary>
    private void AlignmentManager_EventAlignmentFinished(bool success, Vector3 position, float rotation)
    {
        if (hideDuringAlignment && wasActive)
        {
            gameObject.SetActive(true);
        }
    }
}
