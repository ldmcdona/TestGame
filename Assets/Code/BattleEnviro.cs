using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnviro : MonoBehaviour
{
    Vector3 on, off;
    TextBehaviour a1, a2, e1, e2, choice;
    EndTurn[] buttons;
    GameObject cam, temp;
    ShipBehaviour alive;
    EnShip dead;
    // Start is called before the first frame update
    void Start()
    {
        //Random.Range(1, 6);
        buttons = new EndTurn[4];

        on.x = 60;
        off.x = 0;
        on.y = off.y = 0;
        on.z = off.z = -10;
        temp = transform.GetChild(0).gameObject;
        a1 = temp.transform.GetChild(0).gameObject.GetComponent<TextBehaviour>();
        a2 = temp.transform.GetChild(1).gameObject.GetComponent<TextBehaviour>();
        e1 = temp.transform.GetChild(2).gameObject.GetComponent<TextBehaviour>();
        e2 = temp.transform.GetChild(3).gameObject.GetComponent<TextBehaviour>();
        choice = temp.transform.GetChild(4).gameObject.GetComponent<TextBehaviour>();
        temp = transform.GetChild(1).gameObject;
        buttons[0] = temp.transform.GetChild(0).gameObject.GetComponent<EndTurn>();
        buttons[1] = temp.transform.GetChild(1).gameObject.GetComponent<EndTurn>();
        buttons[2] = temp.transform.GetChild(2).gameObject.GetComponent<EndTurn>();
        buttons[3] = temp.transform.GetChild(3).gameObject.GetComponent<EndTurn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(buttons[0].clicked){
                choice.changeText("Heavy Attack");
                buttons[0].foo();
                combat(2, -2);
            }
            if(buttons[1].clicked){
                choice.changeText("Medium Attack");
                buttons[1].foo();
                combat(0, 0);
            }
            if(buttons[2].clicked){
                choice.changeText("Light Attack");
                buttons[2].foo();
                combat(-2, 2);
            }
            if(buttons[3].clicked){
                choice.changeText("Retreat");
                buttons[3].foo();
                escape();
            }
        } 
    }

    void combat(int a, int d){
        int p1a, p2a, p1d, p2d;
        p1a = Random.Range(1, 6);
        p2a = Random.Range(1, 6);
        p1d = Random.Range(1, 6);
        p2d = Random.Range(1, 6);
        
        p1a += alive.attack + a;
        p2a += dead.attack;
        p1d += alive.defense + d;
        p2d += dead.defense;

        Debug.Log("Ally rolled: " + p1a + " " + p1d + ". Enemy rolled: " + p2a + " " + p2d + ".");

        if(p1a > p2d){
            if(dead.damage(1)){
                cam.transform.position = off;
                dead.die();
                Debug.Log("Enemy Destroied");
                p1d = 100;
            }
        }
        if(p2a > p1d){
            if(alive.damage(1)){
                cam.transform.position = off;
                alive.die();
                Debug.Log("Ally Destroied");                
            }
        }
        a2.changeText(alive.desc);
        e2.changeText(dead.desc);
    }

    void escape(){
        int p1s, p2s;
        p1s = Random.Range(1, 6);
        p2s = Random.Range(1, 6);

        p1s += alive.speed;
        p2s += dead.speed;
        if(p1s > p2s){
            Debug.Log("Ship Escaped.");
            cam.transform.position = off;
        } else {
            combat(-6, 2);
        }
    }

    public void battleStart(GameObject oldCam, ShipBehaviour ally, EnShip enemy){
        cam = oldCam;
        dead = enemy;
        alive = ally;
        cam.transform.position = on;
        a1.changeText(ally.title);
        a2.changeText(ally.desc);
        e1.changeText(enemy.title);
        e2.changeText(enemy.desc);
    }
}
