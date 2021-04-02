using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    
    public bool clicked;

    void Start()
    {
        clicked = false;
    }

    void OnMouseDown(){
        clicked = true;
    }

    public void foo(){
        clicked = false;
    }
}

