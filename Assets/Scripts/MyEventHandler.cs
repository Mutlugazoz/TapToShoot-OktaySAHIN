using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateChangedDelegate(GameStates previousState, GameStates newState);
public delegate void TocuhEvent(Vector3 touchLocation);
public delegate void ProjectileStrategyEvent(string strategy);

public delegate void StandardEvent();

public class MyEventHandler : MonoBehaviour
{
    public event StateChangedDelegate GameStateChanged;

    public event TocuhEvent TouchStart;

    public event TocuhEvent Touch;

    public event TocuhEvent TouchEnd;

    public event StandardEvent BlockGotBroken;

    public event ProjectileStrategyEvent ProjectileStrategyChanged;

    public void TriggerStateChange(GameStates previousState, GameStates newState)
    {
        if(GameStateChanged != null)
            GameStateChanged(previousState, newState);
    }

    public void TriggerTouchStart(Vector3 touchLocation)
    {
        if(TouchStart != null)
            TouchStart(touchLocation);
    }

    public void TriggerTouch(Vector3 touchLocation)
    {
        if(Touch != null)
            Touch(touchLocation);
    }

    public void TriggerTouchEnd(Vector3 touchLocation)
    {
        if(TouchEnd != null)
            TouchEnd(touchLocation);
    }

    public void TriggerBlockGotBroken() {
        if(BlockGotBroken != null)
            BlockGotBroken();
    }

    public void TriggerNewProjectileStrategy(string strategy) {
        ProjectileStrategyChanged(strategy);
    }
}
