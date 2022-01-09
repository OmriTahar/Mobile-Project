using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Renderer myRenderer;
    private Animator myAnimator;

    [Header("References")]
    public AudioManager audioManager;
    public ParticleSystem HitEffect;
    public Material OffMat;
    public Material StandbyMat;
    public Material OnMat;

    [Header("Information")]
    public bool isTriggered = false;
    public bool isCounted = false;
    public GameObject TriggerToTurnOff;

    [Header("Switch Target")]
    public Target SecondSwitch;


    private void Start()
    {
        myRenderer = GetComponent<Renderer>();

        if (GetComponentInParent<Animator>() != null)
        {
            myAnimator = GetComponentInParent<Animator>();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 && gameObject.layer == 8)
        {
            audioManager.PlaySound("TargetHit");

            if (TriggerToTurnOff != null)
                Destroy(TriggerToTurnOff);
            
            if (HitEffect.gameObject != null)
                HitEffect.Play();

            if (myAnimator != null)
                myAnimator.SetBool("gotHit", true);

            isTriggered = true;
            ChangeToStandby();

            gameObject.layer = 0;
        }

        if (collision.gameObject.layer == 9 && gameObject.layer == 10 && !isTriggered)
        {
            isTriggered = true;
            SecondSwitch.isTriggered = false;
            audioManager.PlaySound("Door Open");
        }
    }

    public void ChangeToOff()
    {
        myRenderer.material = OffMat;
    }

    public void ChangeToStandby()
    {
        myRenderer.material = StandbyMat;
    }

    public void ChangeToOn()
    {
        myRenderer.material = OnMat;
    }


}
