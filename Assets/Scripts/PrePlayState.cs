using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePlayState : IState
{
    
    private MyGameManager gameManager;
    private GeneralSettings generalSettings;
    private LevelSettings levelSettings;
    private float worldRectangleX;
    private float worldRectangleY;
    private float blockEgdeLength;
    private int totalPriority;
    private MyEventHandler eventHandler;

    public PrePlayState(MyGameManager gameManager, MyEventHandler eventHandler, GeneralSettings generalSettings, LevelSettings levelSettings) 
    {
        this.gameManager = gameManager;
        this.generalSettings = generalSettings;
        this.levelSettings = levelSettings;

        this.eventHandler = eventHandler;
    }

    public void BeginState() 
    {
        calculateEdgeLengthsOfWorldRectangle();
        calculateEdgeLengthOfABlock();
        tileGrid();

        eventHandler.TouchStart += TriggerGameStart;
    }

    public void EndState() 
    {
        eventHandler.TouchStart -= TriggerGameStart;
    }

    public void DoState()
    {
        
    }

    private void calculateEdgeLengthsOfWorldRectangle() 
    {
        Vector3 bottomLeft;
        Vector3 topLeft;
        Vector3 bottomRight;

        Vector3 bottomLeftScreenPoint = new Vector3(
            (Screen.width - (Screen.width * generalSettings.playableScreenAreaRatioX)) * 0.5f,
            (Screen.height - (Screen.height * generalSettings.playableScreenAreaRatioY)) * 0.5f,
            0
            );
        
        Vector3 topLeftScreenPoint = bottomLeftScreenPoint + new Vector3(0, Screen.height - bottomLeftScreenPoint.y, 0);
        Vector3 bottomRightScreenPoint = new Vector3(Screen.width - bottomLeftScreenPoint.x, 0, 0);

        RaycastHit hit;

        Physics.Raycast(Camera.main.ScreenPointToRay(bottomLeftScreenPoint), out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("PrePlayRaycast"));
        bottomLeft = hit.point;

        Physics.Raycast(Camera.main.ScreenPointToRay(topLeftScreenPoint), out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("PrePlayRaycast"));
        topLeft = hit.point;

        
        Physics.Raycast(Camera.main.ScreenPointToRay(bottomRightScreenPoint), out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("PrePlayRaycast"));
        bottomRight = hit.point;

        worldRectangleX = (bottomRight.x - bottomLeft.x);
        worldRectangleY = (topLeft.y - bottomLeft.y);

    }

    private void calculateEdgeLengthOfABlock() 
    {
        float worldRectangleXDividedByY = worldRectangleX / worldRectangleY;
        
        float gridXDividedByY = (float)levelSettings.numberOfHorizontalBlocks / (float)levelSettings.numberOfVerticalBlocks;

        if(gridXDividedByY >= worldRectangleXDividedByY)
        {
            blockEgdeLength = worldRectangleX / levelSettings.numberOfHorizontalBlocks;
        }
        else
        {
            blockEgdeLength = worldRectangleY / levelSettings.numberOfVerticalBlocks;
        }
    }

    private void tileGrid()
    {
        float mostLeft = levelSettings.numberOfHorizontalBlocks % 2 == 1 ? 
        (levelSettings.numberOfHorizontalBlocks / 2) * - blockEgdeLength
        : 
        (levelSettings.numberOfHorizontalBlocks / 2) * - blockEgdeLength + blockEgdeLength * 0.5f;

        float mostBottom = levelSettings.numberOfVerticalBlocks % 2 == 1 ? 
        (levelSettings.numberOfVerticalBlocks / 2) * - blockEgdeLength
        : 
        (levelSettings.numberOfVerticalBlocks / 2) * - blockEgdeLength + blockEgdeLength * 0.5f;

        CalculateTotalPriority();

        int breakableBlockCount = 0;

        for(int i = 0; i < levelSettings.numberOfVerticalBlocks; i++)
        {
            for(int j = 0; j < levelSettings.numberOfHorizontalBlocks; j++) {
                GameObject newBlock = GameObject.Instantiate(decideNextBlockType());
                newBlock.transform.position = new Vector3(mostLeft + j * blockEgdeLength, mostBottom + i * blockEgdeLength, 0);
                newBlock.transform.localScale = new Vector3(blockEgdeLength, blockEgdeLength, blockEgdeLength) * 0.9f;

                if(newBlock.GetComponent<IBreakableTile>() != null)
                    breakableBlockCount++;
            }
        }

        gameManager.SetBreakableBlockCount(breakableBlockCount);
    }

    private void CalculateTotalPriority() {
        int sum = 0;
        for(int i = 0; i < levelSettings.levelBlocks.Count; i ++)
            sum += levelSettings.levelBlocks[i].priority;

        totalPriority = sum;
    }

    private GameObject decideNextBlockType() {

        int nextBlockPosition = Random.Range(0, (totalPriority - 1));
        int counter = 0;

        GameObject returningPrefab = levelSettings.levelBlocks[0].blockPrefab;

        while(nextBlockPosition >= 0) 
        {
            returningPrefab = levelSettings.levelBlocks[counter].blockPrefab;

            nextBlockPosition -= levelSettings.levelBlocks[counter].priority;

            counter++;
        }

        return returningPrefab;
    }

    private void TriggerGameStart(Vector3 touchLocation)
    {
        gameManager.switchState(GameStates.DuringPlay);
    }

}
