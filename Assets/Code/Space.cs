using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public int x, y;
    public bool clicked, occupied, hostile;
    //ShipBehaviour ship;

    void Start(){
        clicked = false;
        occupied = false;
        hostile = false;
    }

    void OnMouseDown(){
        clicked = true;
        Debug.Log("I am space " + x + " " + y);
    }

    public void foo(){
        clicked = false;
    }

    public void coord(int a, int b){
        x = a;
        y = b;
    }

    public Vector3 getPos(){
        return transform.position;
    }

    public void setOccupied(){
        occupied = true;
    }

    public void toggleHostile(){
        if(hostile){
            hostile = false;
        } else {
            hostile = true;
        }
    }
}
