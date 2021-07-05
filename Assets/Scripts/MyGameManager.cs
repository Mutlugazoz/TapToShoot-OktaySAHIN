using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public GeneralSettings generalSettings;
    public LevelSettings levelSettings;

    private IState prePlayState;
    private IState duringPlayState;
    private IState postPlayState;
    private IState currentStatePointer;
    private GameStates currentStateEnum = GameStates.PrePlay;
    private MyEventHandler eventHandler;
    private int breakableBlockCount;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 10;
        eventHandler = FindObjectOfType<MyEventHandler>();

        prePlayState = new PrePlayState(this, eventHandler, generalSettings, levelSettings);
        duringPlayState = new DuringPlayState(this, eventHandler);
        postPlayState = new PostPlayState(this, eventHandler);

        currentStatePointer = prePlayState;
        currentStatePointer.BeginState();

        eventHandler.BlockGotBroken += TrackRemainingBlocks;
    }

    void Update()
    {
        currentStatePointer.DoState();
    }

    public void switchState(GameStates newState) 
    {
        if(newState != currentStateEnum) 
        {
            currentStatePointer.EndState();

            switch(newState) 
            {
                case GameStates.PrePlay:
                    currentStatePointer = prePlayState;
                    break;

                case GameStates.DuringPlay:
                    currentStatePointer = duringPlayState;
                    break;

                case GameStates.PostPlay:
                    currentStatePointer = postPlayState;
                    break;

                default:
                    break;
            }

            eventHandler.TriggerStateChange(currentStateEnum, newState);
            currentStateEnum = newState;

            currentStatePointer.BeginState();

        }


    }

    public void SetBreakableBlockCount(int count)
    {
        breakableBlockCount = count;
    }

    private void TrackRemainingBlocks()
    {
        breakableBlockCount--;
        
        if(breakableBlockCount <= 0)
            switchState(GameStates.PostPlay);
    }

}

public enum GameStates { PrePlay, DuringPlay, PostPlay }