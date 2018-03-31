// problem: in each Button's Awake() function, it assigns its own default position to return the visual button to after it is let up. However, in some of the
// components this mod adds, the default position must be changed.
// Solution: patch the ButtonDown and ButtonUp methods, and handle button position changes in a new way.

// This is an incredibly awkward shitty hacky way of doing this. The reason for this is twofold: firstly, I don't know how to use this framework properly, and secondly,
// I didn't write the game's code in a way that anticipated modding. The second issue is one I intend to fix in future versions of TUNG.


using PiTung;
using UnityEngine;

[Target(typeof(Button))]
public class ButtonPatch
{
    private static GameObject VisualButtonBeingPressed;
    private static Vector3 OriginalLocalPositionOfVisualButtonBeingPressed;

    [PatchMethod(OriginalMethod = "ButtonDown", PatchType = PatchType.Prefix)]
    static void ButtonDownPre()
    {
        RaycastHit hit;
        Physics.Raycast(FirstPersonInteraction.Ray(), out hit, Settings.ReachDistance);
        VisualButtonBeingPressed = hit.collider.GetComponent<Button>().visualbutton;
        OriginalLocalPositionOfVisualButtonBeingPressed = VisualButtonBeingPressed.transform.localPosition;
    }

    [PatchMethod(OriginalMethod = "ButtonDown", PatchType = PatchType.Postfix)]
    static void ButtonDownPost()
    {
        VisualButtonBeingPressed.transform.localPosition = OriginalLocalPositionOfVisualButtonBeingPressed - new Vector3(0, 0.15f, 0);
    }

    [PatchMethod(PatchType = PatchType.Postfix)]
    static void ButtonUp()
    {
        VisualButtonBeingPressed.transform.localPosition = OriginalLocalPositionOfVisualButtonBeingPressed;
    }
}