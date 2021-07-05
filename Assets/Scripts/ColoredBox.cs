using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBox : MonoBehaviour, IBreakableTile
{
    private MyEventHandler eventHandler;
    private Rigidbody rigidBody;
    private Renderer colorRenderer;
    public float destructionDelay = 1f;
    private bool isHit = false;
    private Vector3 forceToApply = new Vector3(0, 0, 5);
    
    void Start()
    {
        
        colorRenderer = GetComponent<MeshRenderer>();

        AssignColor();

        rigidBody = GetComponent<Rigidbody>();

        eventHandler = FindObjectOfType<MyEventHandler>();
        eventHandler.BlockGotBroken += AssignColor;
    }

    public void GetBroken()
    {
        if(!isHit)
        {
            isHit = true;

            eventHandler.BlockGotBroken -= AssignColor;

            eventHandler.TriggerBlockGotBroken();

            rigidBody.isKinematic = false;
            rigidBody.AddForce(forceToApply);

            StartCoroutine(destroyCoroutine());
        }
        
    }

    public bool IsBroken()
    {
        return isHit;
    }

    private void AssignColor() 
    {
        colorRenderer.material.SetColor("_BaseColor", new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)));
    }

    private IEnumerator destroyCoroutine()
    {
        yield return new WaitForSeconds(destructionDelay);
        Destroy(gameObject);
    }
}
