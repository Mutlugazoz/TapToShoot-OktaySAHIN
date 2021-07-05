using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void BeginState();
    void EndState();
    void DoState();
}
