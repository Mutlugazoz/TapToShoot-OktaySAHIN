using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GeneralSettings : ScriptableObject
{
    [Range(0, 1)]
    public float playableScreenAreaRatioX;
    [Range(0, 1)]
    public float playableScreenAreaRatioY;
}
