using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuringPlayState : IState
{
    // Start is called before the first frame update
    private MyGameManager gameManager;
    private MyEventHandler eventHandler;
    private string projectileStrategy = "StandardProjectile";

    public DuringPlayState(MyGameManager gameManager, MyEventHandler eventHandler) 
    {
        this.gameManager = gameManager;
        this.eventHandler = eventHandler;
    }

    public void BeginState() 
    {
        eventHandler.TouchStart += FireProjectile;
        eventHandler.ProjectileStrategyChanged += TrackProjectileStrategy;
    }

    public void EndState() 
    {
        eventHandler.TouchStart -= FireProjectile;
        eventHandler.ProjectileStrategyChanged -= TrackProjectileStrategy;
    }

    public void DoState()
    {
        
    }

    private void FireProjectile(Vector3 touchLocation)
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(touchLocation), out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Breakable")))
        {
            IBreakableTile tile = hit.collider.transform.root.GetComponent<IBreakableTile>();

            if(tile != null)
            {
                if(!tile.IsBroken())
                {
                    Vector3 direction = Vector3.Normalize(hit.point - Camera.main.transform.position);

                    GameObject projectile = ObjectPooler.Instance.SpawnObject(projectileStrategy);
                    projectile.transform.position = Camera.main.transform.position;
                    projectile.GetComponent<IProjectile>().Fire(direction);
                }
            }
            
        }
    }

    private void TrackProjectileStrategy(string strategy)
    {
        projectileStrategy = strategy;
    }


}
