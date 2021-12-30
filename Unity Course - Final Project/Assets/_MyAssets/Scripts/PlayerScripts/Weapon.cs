using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Weapon Settings")]
    [SerializeField] private float _bulletSpeed = 10;

    [Header("Ammunition and Reloading")]
    public int _maxAmmo = 42;
    public int _currentAmmo = 28;
    public int _magSize = 14;
    public int _magCurrentAmmo = 14;


    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "Shoot";

    [Header("References")]
    public Transform Camera;
    public Transform FirePoint;
    public ParticleSystem MuzzleFlash;

    [Header("Information")]
    [SerializeField] bool CanShoot = true;
    [SerializeField] private bool _isShooting;

    BulletPool _bulletPool;


    void Start()
    {
        _bulletPool = BulletPool.Instance;
    }

    public void Shoot()
    {
        Vector3 bulletVelocity = Camera.forward * _bulletSpeed;
        _bulletPool.PickFromPool(FirePoint.position, bulletVelocity);

        MuzzleFlash.Play();
        FindObjectOfType<AudioManager>().PlaySound("Pistol Shot Cut");

        animator.SetTrigger(animatorParam);
        Invoke("CanShootFunc", 0.4f);
    }

    public void PullTrigger()
    {
        if (CanShoot)
        {
            _isShooting = true;
            CanShoot = false;
            Shoot();
        }
    }

    public void ReleaseTrigger()
    {
        _isShooting = false;
    }

    public void CanShootFunc()
    {
        CanShoot = true;
    }
}
