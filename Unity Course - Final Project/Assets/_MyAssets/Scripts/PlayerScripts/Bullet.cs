using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float lifeTime;
    public GameObject hitEffect;

    public void Activate(Vector3 position, Vector3 velocity)
    {
        transform.position = position;
        rigidbody.velocity = velocity;
        gameObject.SetActive(true);

        StartCoroutine("Decay");
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(lifeTime);
        Deactivate();
    }

    public void Deactivate()
    {
        BulletPool.Instance.AddToPool(this);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform excectHitPoint = gameObject.transform;
        print("Object Name:" + name + " ,Positon: " + excectHitPoint.position + " ,Rotation: " +  excectHitPoint.rotation);

        SpawnHitEffect(excectHitPoint, hitEffect, collision.transform);
        Deactivate();
    }

    void SpawnHitEffect(Transform hitPoint, GameObject effectPrefab, Transform newParent)
    {
        GameObject spawnedEffect = GameObject.Instantiate(effectPrefab, hitPoint.position, Quaternion.LookRotation(hitPoint.forward));
        spawnedEffect.transform.SetParent(newParent.transform);
    }
}
