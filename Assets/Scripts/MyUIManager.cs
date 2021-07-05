using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUIManager : MonoBehaviour
{
    private MyEventHandler eventHandler;
    public List<GameObject> prePlayUIs;
    public List<GameObject> duringPlayUIs;
    public List<GameObject> postPlayUIs;

    // Start is called before the first frame update
    void Start()
    {
        eventHandler = FindObjectOfType<MyEventHandler>();
        eventHandler.GameStateChanged += TrackGameState;
    }

    private void TrackGameState(GameStates previous, GameStates next)
    {
        switch(next)
        {
            case GameStates.DuringPlay:
                GameStarted();
                break;

            case GameStates.PostPlay:
                GameEnded();
                break;
            
            default:
                break;
        }
    }
    
    private void GameStarted()
    {
        prePlayUIs.ForEach(delegate(GameObject go) {
            go.SetActive(false);
        });

        duringPlayUIs.ForEach(delegate(GameObject go) {
            go.SetActive(true);
        });
    }

    private void GameEnded()
    {
        duringPlayUIs.ForEach(delegate(GameObject go) {
            go.SetActive(false);
        });

        postPlayUIs.ForEach(delegate(GameObject go) {
            go.SetActive(true);
        });
    }
}
