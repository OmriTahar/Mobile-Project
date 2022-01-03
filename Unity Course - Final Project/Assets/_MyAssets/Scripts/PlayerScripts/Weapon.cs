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
    public TextMeshProUGUI AmmoText;

    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "Shoot";

    [Header("References")]
    public GameManager gameManager;
    public AudioManager audioManager;
    public Transform Camera;
    public Transform FirePoint;
    public ParticleSystem MuzzleFlash;

    [Header("Information")]
    public bool _isInfiniteAmmo = true;
    public bool _isReloading = false;
    [SerializeField] bool CanShoot = true;
    private bool _isShooting;

    BulletPool _bulletPool;
    int ammoSpaceToFill = 0;
    int ammoToReload = 0;
    private bool _hasLost = false;


    void Start()
    {
        _bulletPool = BulletPool.Instance;

        if (_isInfiniteAmmo)
        {
            AmmoText.gameObject.SetActive(false);
        }
        else
        {
            AmmoText.gameObject.SetActive(true);
            AmmoText.text = _magCurrentAmmo + "/" + SpareAmmo;
        }
    }

    private void Update()
    {
        if (_magCurrentAmmo <= 0 && SpareAmmo <= 0 && !_hasLost) // Lose Condition
        {
            _hasLost = true;
            gameManager.GameOver();
        }
    }

    private void ShootFlow()
    {

        _magCurrentAmmo -= 1;
        AmmoText.text = _magCurrentAmmo + "/" + SpareAmmo;

        Vector3 bulletVelocity = Camera.forward * _bulletSpeed;
        _bulletPool.PickFromPool(FirePoint.position, bulletVelocity);

        MuzzleFlash.Play();
        audioManager.PlaySound("Pistol Shot Cut");

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
            audioManager.PlaySound("Pistol Reload");

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
        AmmoText.text = _magCurrentAmmo + "/" + SpareAmmo;

        CanShoot = true;
        _isReloading = false;
        animatorParam = "Shoot";
    }
}
