using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour, IProjectile, IPoolItem
{

    public float fireSpeed;
    public float explosionRange = 5;
    private Rigidbody projectileRB;
    private SphereCollider trigger;
    private List<IBreakableTile> tilesInRange = new List<IBreakableTile>();
    public void OnStart() 
    {
        tilesInRange.Clear();

        projectileRB = GetComponent<Rigidbody>();
        SphereCollider[] colliders = GetComponents<SphereCollider>();

        for(int i = 0; i < colliders.Length; i++)
            if(colliders[i].isTrigger)
            {
                trigger = colliders[i];
                break;
            }

        trigger.radius = explosionRange * 0.5f;
                
    }

    public void Fire(Vector3 direction)
    {
        projectileRB.velocity = direction * fireSpeed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        IBreakableTile tile = other.transform.root.GetComponent<IBreakableTile>();
        
        if(tile != null)
        {
            tilesInRange.Add(tile);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        tilesInRange.ForEach(delegate (IBreakableTile tile) {
            tile.GetBroken();
        });
        gameObject.SetActive(false);
    }
}
