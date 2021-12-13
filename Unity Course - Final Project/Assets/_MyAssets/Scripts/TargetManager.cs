using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    public List<Target> TargetList = new List<Target>();
    public int triggerCounter = 0;


    void Update()
    {
        foreach (var target in TargetList)
        {
            if (target.isTriggered)
            {
                triggerCounter++;
            }
        }


        if (triggerCounter == TargetList.Count)
        {
            foreach (var target in TargetList)
            {
                target.ChangeToOn();
            }
        }
    }
}
