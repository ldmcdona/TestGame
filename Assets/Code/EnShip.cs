using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnShip : MonoBehaviour
{
    public string title, phil,  desc;
    public int x, y;
    public int speed, attack, defense, health;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    public void StartUp(int s, int a, int d, int h, string c){
        speed = s;
        attack = a;
        defense = d;
        health = h;
        phil = c;
        desc = phil + "\nSpeed: " + speed + "\nAttack: " + attack + "\nDefense: " + defense + "\nHealth: " + health;
    }

    public void die(){
        Destroy(gameObject);
    }

    public bool damage(int i){
        health -= i;
        if(health > 0){
            desc = phil + "\nSpeed: " + speed + "\nAttack: " + attack + "\nDefense: " + defense + "\nHealth: " + health;
            return false;
        } else {
            return true;
        }
    }
}
