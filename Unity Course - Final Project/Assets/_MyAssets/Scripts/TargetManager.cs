using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    [Header("Targets")]
    public List<Target> TargetList = new List<Target>();
    public int triggerCounter = 0;
    public bool isAllTriggered = false;
    [Header("Door Access")]
    public DoorAccess DoorAccess;

    void Update()
    {

        if (!isAllTriggered)
        {
            foreach (var target in TargetList)
            {
                if (target.isTriggered && !target.isCounted)
                {
                    triggerCounter++;
                    target.isCounted = true;
                }
            }

            if (triggerCounter == TargetList.Count)
            {
                foreach (var target in TargetList)
                {
                    target.ChangeToOn();
                    isAllTriggered = true;
                    gameObject.layer = 7;
                }
            }
        }

        if (isAllTriggered && !DoorAccess.isAccessedDoor)
        {
            DoorAccess.ChangeActiveness(DoorAccess.OffPhase, DoorAccess.StandByPhase);
        }
        else if (isAllTriggered && !DoorAccess.isAccessedDoor)
        {
            DoorAccess.ChangeActiveness(DoorAccess.StandByPhase, DoorAccess.OnPhase);
        }

    }

}
