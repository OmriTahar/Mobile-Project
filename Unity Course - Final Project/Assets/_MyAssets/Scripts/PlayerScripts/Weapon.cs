using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{

    [Header("Weapon Settings")]
    [SerializeField] private float _bulletSpeed = 10;

    [Header("Ammunition and Reloading")]
    public int _maxAmmo = 20;
    public int _currentAmmo = 10;
    public int _magSize = 3;
    public int _magCurrentAmmo = 3;
    public bool _isReloading = false;
    public TextMeshProUGUI AmmoText;


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

    private void Update()
    {
        AmmoText.text = _magCurrentAmmo + "/" + _currentAmmo;
        
    }


    private void ShootFlow()
    {
        _magCurrentAmmo -= 1;

        Vector3 bulletVelocity = Camera.forward * _bulletSpeed;
        _bulletPool.PickFromPool(FirePoint.position, bulletVelocity);

        MuzzleFlash.Play();
        FindObjectOfType<AudioManager>().PlaySound("Pistol Shot Cut");

        animator.SetTrigger(animatorParam);
    }

    public void Shoot()
    {
        if (_magCurrentAmmo > 1 && !_isReloading)
        {
            ShootFlow();
            Invoke("CanShootFunc", 0.5f);
        }
        else if (_magCurrentAmmo == 1 && !_isReloading)
        {
            ShootFlow();
            Invoke("Reload", 0.5f);
        }
    }

    public void Reload()
    {
        int ammoSpaceToFill = 0;
        int ammoToReload = 0;

        // Actual Reloading
        if (_magCurrentAmmo < _magSize && _currentAmmo >= 1)
        {
            _isReloading = true;
            CanShoot = false;

            animatorParam = "Reload";
            animator.SetTrigger(animatorParam);

            ammoSpaceToFill = _magSize - _magCurrentAmmo;

            // Ammo to Reload check
            if (ammoSpaceToFill >= _currentAmmo)
            {
                ammoToReload = _currentAmmo;
            }
            else if (ammoSpaceToFill < _currentAmmo)
            {
                ammoToReload = ammoSpaceToFill;
            }

            _currentAmmo -= ammoToReload;
            _magCurrentAmmo += ammoToReload;

            Invoke("DoneReloading", 1.4f);
        }
        else
        {
            Debug.Log("No ammo left");
        }

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

    public void DoneReloading()
    {
        CanShoot = true;
        _isReloading = false;
        animatorParam = "Shoot";
    }
}
