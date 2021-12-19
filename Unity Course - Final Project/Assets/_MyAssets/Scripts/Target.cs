using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Renderer myRenderer;

    [Header("References")]
    public ParticleSystem HitEffect;
    public Material StandbyMat;
    public Material OnMat;
    [Header("Information")]
    public bool isTriggered = false;
    public bool isCounted = false;

    public GameObject TriggerToTurnOff;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    public void ChangeToStandby()
    {
        myRenderer.material = StandbyMat;
    }

    public void ChangeToOn()
    {
        myRenderer.material = OnMat;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && gameObject.layer == 8)
        {
            HitEffect.Play();
            isTriggered = true;
            
            ChangeToStandby();

            if (TriggerToTurnOff != null)
            {
                Destroy(TriggerToTurnOff);
            }

            gameObject.layer = 0;
        }
    }
}
