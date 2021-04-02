using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipBehaviour : MonoBehaviour
{
    public int x, y, moves = 3;
    public bool clicked;
    private Vector3 direction;
    private Space prev;
    public string title, phil, desc;
    public int speed, attack, defense, health;

    public void Start(){

        //All these stats are temporary. Will change once ship spawner is added.
        speed = 1;
        attack = 2;
        defense = 2;
        health = 4;
        phil = "Light Cruiser";
        desc = "Speed: " + speed + "\nAttack: " + attack + "\nDefense: " + defense + "\nHealth: " + health;
        
        clicked = false;
    }

    //For when I eventually have a friendly ship spawner.
    public void StartUp(int s, int a, int d, int h, string c){
        speed = s;
        attack = a;
        defense = d;
        health = h;
        phil = c;
        desc = "Speed: " + speed + "\nAttack: " + attack + "\nDefense: " + defense + "\nHealth: " + health;
    }

    public void OnMouseDown(){
        Debug.Log("Ship clicked");
        clicked = true;
    }

    public void foo(){
        clicked = false;
    }

    public void endTurn(){
        moves = 3;
    }

    public void setPrev(Space temp){
        direction = temp.getPos();
        direction.z--;
        transform.position = direction;
        prev = temp;
        x = prev.x;
        y = prev.y;
    }

    public bool damage(int i){
        health -= i;
        if(health > 0){
            desc = "Speed: " + speed + "\nAttack: " + attack + "\nDefense: " + defense + "\nHealth: " + health;
            return false;
        } else {
            return true;
        }
    }

    public bool movement(Space space){
        //Space sb = space.GetComponent<Space>();
        if(space.occupied == false){
            int disX = space.x - x, disY = space.y - y;
            int distance;
            if((disX < 0 && disY < 0) || (disX > 0 && disY > 0)){
                distance = Math.Max(Math.Abs(disX), Math.Abs(disY));
            } else {
                distance = Math.Abs(disX) + Math.Abs(disY);
            }
            if(distance <= moves){
                moves -= distance;
                x = space.x; 
                y = space.y;
                direction = space.getPos();
                direction.z--;
                transform.position = direction;
                prev.occupied = false;
                space.occupied = true;
                prev = space;
                return true;
            } else {
                return false;
            }
        } else {
            Debug.Log("Space Occupied.");
            return false;
        }
    }

    public Vector3 getPos(){
        return transform.position;
    }

    public void die(){
        Destroy(gameObject);
    }
}
