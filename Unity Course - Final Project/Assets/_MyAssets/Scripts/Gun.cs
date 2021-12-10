using UnityEngine;

public class Gun : MonoBehaviour
{

    public float Damage = 10f;
    public float Range = 100f;

    public GameObject ShootingPoint;


    void Update()
    {
        
    }

    public void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ShootingPoint.transform.position, ShootingPoint.transform.forward, out hitInfo, Range))
        {
            Debug.Log(hitInfo.transform.name);
        }
        
    }
}
