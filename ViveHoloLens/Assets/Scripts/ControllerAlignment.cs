// ------------------------------------
// SCRIPT      : ControllerAlignment.cs
// CREATE DATE : 20.01.2017
// PURPOSE     : 
// 
// AUTHOR       : Drew Gottlieb
// MODIFIED BY  : Mîchel GABRIEL 
// ------------------------------------
using UnityEngine;

public class ControllerAlignment : MonoBehaviour
{
    public AlignmentManager alignmentManager;

    private SteamVR_TrackedController controller;

    void Start()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        if (!controller)
        {
            Debug.LogError("Controller must have SteamVR_TrackedController behavior.");
            return;
        }
        controller.TriggerClicked += Controller_TriggerClicked;
    }

    private void Controller_TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (alignmentManager.CurrentlyAligning)
        {
            GetComponent<AllowToDraw>().HoloLensPlayer = true;
            Utils.HoloPlayer = GetComponent<AllowToDraw>().player;
            alignmentManager.ControllerClicked(this.transform);
        }
    }
}
