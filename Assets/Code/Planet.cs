using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private int growth, pop;
    public bool clicked;
    public string title, desc;
    // Start is called before the first frame update
    void Start(){
        clicked = false;
        growth = 0;
        pop = 1;
    }

    public void foo(){
        clicked = false;
    }

    public void OnMouseDown(){
        Debug.Log("Planet clicked");
        clicked = true;
    }

    public void movement(Space space){
        transform.position = space.getPos();
        Vector3 temp = transform.position;
        temp.z = temp.z - 1;
        transform.position = temp;
    }

    public void endTurn(){
        if(pop < 3){
            growth++;
            if(growth == 5){
                growth = 0;
                pop++;
            }
        }
    }

    public Vector3 getPos(){
        return transform.position;
    }

    public int getPop(){
        return pop;
    }
}
