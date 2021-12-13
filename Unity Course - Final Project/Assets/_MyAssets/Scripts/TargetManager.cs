using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    public List<Target> TargetList = new List<Target>();
    public int triggerCounter = 0;
    public bool isAllTriggered = false;

    public GameObject DoorAccessOff;
    public GameObject DoorAccessStandBy;

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

        if (isAllTriggered)
        {
            DoorAccessOff.SetActive(false);
            DoorAccessStandBy.SetActive(true);
        }
    }

}
