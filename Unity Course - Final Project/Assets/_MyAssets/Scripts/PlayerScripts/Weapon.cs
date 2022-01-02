using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{

    [Header("Weapon Settings")]
    [SerializeField] private float _bulletSpeed = 10;

    [Header("Ammunition and Reloading")]
    public int MaxHoldableAmmo = 20;
    public int _magSize = 3;
    public int SpareAmmo = 10;
    public int _magCurrentAmmo = 3;
    public bool _isReloading = false;
    public TextMeshProUGUI AmmoText;

    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "Shoot";

    [Header("References")]
    public GameManager gameManager;
    public Transform Camera;
    public Transform FirePoint;
    public ParticleSystem MuzzleFlash;

    [Header("Information")]
    public bool _isInfiniteAmmo = true;
    [SerializeField] bool CanShoot = true;
    [SerializeField] private bool _isShooting;

    BulletPool _bulletPool;
    int ammoSpaceToFill = 0;
    int ammoToReload = 0;


    void Start()
    {
        _bulletPool = BulletPool.Instance;
    }

    private void Update()
    {
        if (_isInfiniteAmmo)
        {
            AmmoText.gameObject.SetActive(false);
        }
        else
        {
            AmmoText.gameObject.SetActive(true);
            AmmoText.text = _magCurrentAmmo + "/" + SpareAmmo;
        }

        if (_magCurrentAmmo <= 0 && SpareAmmo <= 0)
        {
            gameManager.LoseCondition();
        }
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
            Invoke("CanShootFunc", 1f);
        }
        else if (_magCurrentAmmo == 1 && !_isReloading)
        {
            ShootFlow();
            Invoke("Reload", 0.5f);
        }
    }

    public void Reload()
    {

        if (_magCurrentAmmo < _magSize && SpareAmmo >= 1)
        {
            _isReloading = true;
            CanShoot = false;

            animatorParam = "Reload";
            animator.SetTrigger(animatorParam);
            FindObjectOfType<AudioManager>().PlaySound("Pistol Reload");

            ammoSpaceToFill = _magSize - _magCurrentAmmo;

            // Ammo to Reload check
            if (ammoSpaceToFill >= SpareAmmo)
            {
                ammoToReload = SpareAmmo;
            }
            else if (ammoSpaceToFill < SpareAmmo)
            {
                ammoToReload = ammoSpaceToFill;
            }


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
        if (!_isInfiniteAmmo)
            SpareAmmo -= ammoToReload;

        _magCurrentAmmo += ammoToReload;

        CanShoot = true;
        _isReloading = false;
        animatorParam = "Shoot";
    }
}
