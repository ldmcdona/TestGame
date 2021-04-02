using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    //Not sure what I want to do with this currently.

    //Let's think about a smart grid. 

    //Pretend X and Y are seperate for now, because the basically are. 5x5 hex grid, let's go.

    //Make 5 hexs. Then make 6 hexs. Then 7. Then 8. Then 9. Then 8, 7, 6, 5.
    
    public Space spacePrefab;
    public Space[] grid;
    public int holdingX, holdingY;
    
    public void Awake(){
        Debug.Log("testing");
        holdingX = 4;
        holdingY = 8;
        grid = new Space[61];

        int x = 5, y = 0, c = 0;
        for(int i = 0; i < 9; i++){
            for(int j=0; j < x; j++){
                makeHex(j, i, c);
                c++;
            }
            if(y < 4){
                x++;
            } else {
                x--;
            }
            y++;
        }
        coordinates();
    }
    //rename x and y to other things then just add an x and y counter that tick at different rates.
    //Go back to the python experiment in Documents/stuff/extra if you have to.

    public void makeHex(int x, int y, int i){
        //https://catlikecoding.com/unity/tutorials/hex-map/part-1/
        //Look here for the math and the prefabs and stuff.
        Vector3 position;
        if(y < 5){
            position.x = (x * 0.9f) - (y * 0.45f);
        } else {
            position.x = (x * 0.9f) - ((8 % y) * 0.45f);
        }
        position.y = y * 0.85f;
        position.z = 0f;

        //Pretty sure this line is causing me issues.
        Space cell = grid[i] = Instantiate<Space>(spacePrefab);
        //Might want to do something like:
        //GameObject cell = Instantiate(spacePrefab);
        //Space cb = grid[i] = cell.getComponent<Space>();
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }
    
    public void coordinates(){
        int x = 4, y = 8;
        for(int i=0; i < 61; i++){
            grid[i].coord(x, y);
            x--;
            y--;
            if(y < 0){
                x = holdingX;
                holdingY--;
                y = holdingY;
            }
            if(x < 0){
                holdingX++;
                x = holdingX;
                y = holdingY;
            }
        }
    }
}
