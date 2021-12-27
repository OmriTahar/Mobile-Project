using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{

    [Header("Animator References")]
    public Animator PlatormAnimator;
    public Animator DoorAnimator;

    [Header("Switches References")]
    public Target OpenSwitch;
    public Target CloseSwitch;
    
    void Update()
    {
        if (OpenSwitch.isTriggered)
        {
            OpenSwitch.ChangeToOn();
            CloseSwitch.ChangeToOff();

            PlatormAnimator.SetBool("Close", true);
            PlatormAnimator.SetBool("Open", false);

            DoorAnimator.SetBool("Open", true);
            DoorAnimator.SetBool("Close", false);
        }

        if (CloseSwitch.isTriggered)
        {
            CloseSwitch.ChangeToOn();
            OpenSwitch.ChangeToOff();

            PlatormAnimator.SetBool("Close", false);
            PlatormAnimator.SetBool("Open", true);

            DoorAnimator.SetBool("Open", false);
            DoorAnimator.SetBool("Close", true);
        }
    }
}
