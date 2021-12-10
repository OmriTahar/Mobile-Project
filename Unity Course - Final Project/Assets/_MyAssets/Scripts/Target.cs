using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material myMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            myMaterial.color = Color.green;
            Debug.Log("Chaning to green!");
        }
    }
}
