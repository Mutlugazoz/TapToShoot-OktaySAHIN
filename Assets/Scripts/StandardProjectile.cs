using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour, IProjectile, IPoolItem
{
    public float fireSpeed;
    private Rigidbody projectileRB;
    public void OnStart() 
    {
        projectileRB = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 direction)
    {
        projectileRB.velocity = direction * fireSpeed;
    }

    private void OnCollisionEnter(Collision other) {
        IBreakableTile hitTile = other.transform.root.GetComponent<IBreakableTile>();

        if(hitTile != null)
        {
            hitTile.GetBroken();
        }

        gameObject.SetActive(false);
    }
}
