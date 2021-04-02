using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour
{
    public EnShip enShipPrefab;
    public EnShip[] fleet;
    int shipCount;

    // Start is called before the first frame update
    void Start()
    {
        fleet = new EnShip[5];
        shipCount = 0;
    }

    public EnShip makeShip(Space spawn, int size){
        EnShip evil = fleet[shipCount] = Instantiate<EnShip>(enShipPrefab);
        shipCount++;
        evil.transform.SetParent(transform, false);
        evil.transform.localPosition = transform.position;
        evil.transform.position = spawn.getPos();
        //spawn.toggleHostile();
        evil.x = spawn.x;
        evil.y = spawn.y;
        switch(size){
            case 1:
                evil.title = "Baddie";
                evil.StartUp(4, 1, 1, 2, "Skiff");
                break;
            case 2:
                evil.title = "Meanie";
                evil.StartUp(4, 1, 1, 2, "Barge");
                break;
            default:
                Debug.Log("Hostile Ship Error");
                break;
        }
        return evil;
    }

}