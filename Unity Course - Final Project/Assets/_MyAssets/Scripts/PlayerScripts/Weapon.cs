using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "Shoot";
    public bool CanShoot = true;

    public Transform Camera;
    public Transform FirePoint;
    public ParticleSystem MuzzleFlash;

    [SerializeField] private bool _isShooting;
    [SerializeField] private float _bulletSpeed = 10;
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
        Debug.Log("I am shooting!");

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
