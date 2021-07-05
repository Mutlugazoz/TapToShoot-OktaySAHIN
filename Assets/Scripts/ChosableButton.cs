using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosableButton : MonoBehaviour
{
    public Image backgroundImage; 
    private Color chosenColor = new Color(0.57f, 1f, 0.66f);
    private Color unchosenColor = new Color(0.65f, 0.65f, 0.65f);
    
    public void GetChosen() {
        backgroundImage.color = chosenColor;
    }

    public void GetUnchosen() {
        backgroundImage.color = unchosenColor;
    }
}
