using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{
    private MyEventHandler eventHandler;

    private void Start() 
    {
        eventHandler = FindObjectOfType<MyEventHandler>();    
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            eventHandler.TriggerTouchStart(Input.mousePosition);

        if(Input.GetMouseButton(0))
            eventHandler.TriggerTouch(Input.mousePosition);

        if(Input.GetMouseButtonUp(0))
            eventHandler.TriggerTouchEnd(Input.mousePosition);
    }
}
