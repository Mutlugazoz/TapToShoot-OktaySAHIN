using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class LevelSettings : ScriptableObject
{
    public List<BlockInfo> levelBlocks;
    public int numberOfHorizontalBlocks;
    public int numberOfVerticalBlocks;
}

[Serializable]
public struct BlockInfo
{
    public GameObject blockPrefab;
    public int priority;
}
