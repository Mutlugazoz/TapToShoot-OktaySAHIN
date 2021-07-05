using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostPlayState : IState
{
    private MyGameManager gameManager;
    private MyEventHandler eventHandler;

    public PostPlayState(MyGameManager gameManager, MyEventHandler eventHandler) 
    {
        this.gameManager = gameManager;
        this.eventHandler = eventHandler;
    }

    public void BeginState() 
    {
        eventHandler.TouchStart += TriggerNextLevel;
    }

    public void EndState() 
    {

    }

    public void DoState()
    {
        
    }

    private void TriggerNextLevel(Vector3 touchLocation)
    {
        eventHandler.TouchStart -= TriggerNextLevel;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
