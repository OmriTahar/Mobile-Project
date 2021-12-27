using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject BulletPrefab;
    public int PoolSize = 100;

    private List<Bullet> _availabaleBullets;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _availabaleBullets = new List<Bullet>();

        Bullet bullet = Instantiate(BulletPrefab, transform).GetComponent<Bullet>();
        bullet.gameObject.SetActive(false);

        _availabaleBullets.Add(bullet);
    }

    public void PickFromPool(Vector3 position, Vector3 velocity)
    {
        if (_availabaleBullets.Count < 1 )
            return;

        _availabaleBullets[0].Activate(position, velocity);

        _availabaleBullets.RemoveAt(0);
    }

    public void AddToPool(Bullet bullet)
    {
        if (!_availabaleBullets.Contains(bullet))
            _availabaleBullets.Add(bullet);
    }

}
