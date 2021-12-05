using System.Collections;
using UnityEngine;

public class Aiming : MonoBehaviour
{

    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "aiming";

    [Header("Camera")]
    public Camera camera;
    public float defaultPov, aimingPov;

    [Header("General")]
    public float aimSpeed = 0.2f;

    [Header("Scope")]
    public bool enableScope;
    public MeshRenderer weaponRenderer;
    public GameObject scopeOverlay;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
