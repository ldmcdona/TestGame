using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterBehaviour : MonoBehaviour
{
    GameObject shipSpawner, dropDownC, drdo, highlight, cam;
    GameObject[] productions;
    ShipBehaviour selected_ship;
    Planet selected_planet;
    ShipBehaviour[] ships;
    EndTurn et1;
    GridBehaviour grid;
    Space[] spaces;
    TextBehaviour[] texts;
    Planet[] planets;
    Dropdown[] prod_choices;
    Hostile enSpawner;
    BattleEnviro arena;
    EnShip[] fleet;
    Resources bank;
    int turn;
    int[] ddvalues;
    int shipNum, eneNum;
    Vector3 outB;
    bool once;

    //Alright. Fresh idea: All objects beneath master are sorted into collections. One for ships, one for spaces, one for planets, etc.
    //The real objects themselves are below those category objects. 
    //Only grab the scripts, not the objects themselves. The objects should handle their own movement via scripts.
        //Sub idea: Have them be arrays of fixed size. Ships[5] is the two starter ships with room for 3 more.
        //Texts are their own thing, planets their own thing, etc.

        //Other sub idea: have the third text-box be general information on the thing selected. It prints a variable sent to it, each 
        //object has it's own text variable to send.

        //More sub ideas: Have more helper functions instead of shovign everything into start() and update(). Have like, endturn() and select().

    void getShips(){
        GameObject temp;
        //Hang about. If I'm going to allow for the spawning of new ships, I'm gonna want the game object to spawn them under.
        shipSpawner = transform.GetChild(0).gameObject;
        temp = shipSpawner.transform.GetChild(0).gameObject;
        ships[0] = temp.GetComponent<ShipBehaviour>();
        temp = shipSpawner.transform.GetChild(1).gameObject;
        ships[1] = temp.GetComponent<ShipBehaviour>();
        for(int i = 2; i < 5; i++){
            ships[i] = null;
        }
        //shipNum will probably eventually be part of the shipSpawner object in it's own script.
        shipNum = 2;
    }

    void getTexts(){
        GameObject temp;
        for(int i = 0; i < 5; i++){
            temp = transform.GetChild(3).gameObject;
            temp = temp.transform.GetChild(0).gameObject;
            temp = temp.transform.GetChild(i).gameObject;
            texts[i] = temp.GetComponent<TextBehaviour>();
        }
    }

    void getPlanets(){
        GameObject temp;
        for(int i = 0; i < 3; i++){
            temp = transform.GetChild(4).gameObject;
            temp = temp.transform.GetChild(i).gameObject;
            planets[i] = temp.GetComponent<Planet>();
        }
    }

    void getSpaces(){
        GameObject temp;
        for(int i=0; i<61; i++){
            temp = grid.transform.GetChild(i).gameObject;
            spaces[i] = temp.GetComponent<Space>();
        }
    }

    void getDDMenus(){
        GameObject temp;
        temp = transform.GetChild(5).gameObject;
        for(int i=0; i<3; i++){
            productions[i] = temp.transform.GetChild(i).gameObject;
            prod_choices[i] = productions[i].GetComponent<Dropdown>();
        }
    }

    void Start()
    {
        once = true;
        outB.x = -26;
        outB.y = 12;
        outB.z = 0;

        turn = 1;
        selected_ship = null;
        selected_planet = null;

        spaces = new Space[61];
        ships = new ShipBehaviour[5];
        texts = new TextBehaviour[5];
        planets = new Planet[3];
        fleet = new EnShip[2];
        productions = new GameObject[3];
        prod_choices = new Dropdown[3];
        ddvalues = new int[3];

        getShips();

        grid = transform.GetChild(1).gameObject.GetComponent<GridBehaviour>();

        et1 = transform.GetChild(2).gameObject.GetComponent<EndTurn>();

        getTexts();

        getPlanets();

        getDDMenus();

        getSpaces();

        highlight = transform.GetChild(6).gameObject;

        enSpawner = transform.GetChild(7).gameObject.GetComponent<Hostile>();

        arena = transform.GetChild(8).gameObject.GetComponent<BattleEnviro>();

        bank = transform.GetChild(9).gameObject.GetComponent<Resources>();

        cam = transform.GetChild(10).gameObject;

        ships[0].setPrev(spaces[1]);
        ships[1].setPrev(spaces[40]);  
        planets[0].movement(spaces[11]);
        planets[1].movement(spaces[23]);
        planets[2].movement(spaces[58]);

        Select();
    }

    void Select(){
        PolygonCollider2D x;
        if(selected_ship != null){
            //Debug.Log("Select True");
            for(int i = 0; i < shipNum; i++){
                x = ships[i].GetComponent<PolygonCollider2D>();
                x.enabled = false;
            }
            for(int i = 0; i < 3; i++){
                x = planets[i].GetComponent<PolygonCollider2D>();
                x.enabled = false;
            }
            for(int i = 0; i < 61; i++){
                x = spaces[i].GetComponent<PolygonCollider2D>();
                x.enabled = true;
            }
        } else if(selected_planet != null) {
            for(int i = 0; i < shipNum; i++){
                x = ships[i].GetComponent<PolygonCollider2D>();
                x.enabled = false;
            }
            for(int i = 0; i < 3; i++){
                x = planets[i].GetComponent<PolygonCollider2D>();
                x.enabled = false;
            }
            for(int i = 0; i < 61; i++){
                x = spaces[i].GetComponent<PolygonCollider2D>();
                x.enabled = false;
            }
        } else {
            //Debug.Log("Select False");
            for(int i = 0; i < shipNum; i++){
                x = ships[i].GetComponent<PolygonCollider2D>();
                x.enabled = true;
            }
            for(int i = 0; i < 3; i++){
                x = planets[i].GetComponent<PolygonCollider2D>();
                x.enabled = true;
            }
            for(int i = 0; i < 61; i++){
                x = spaces[i].GetComponent<PolygonCollider2D>();
                x.enabled = false;
            }            
        }
    }

    void EndTurn(){
        for(int i=0; i<3; i++){
            ddvalues[i] = prod_choices[i].value;
            planets[i].endTurn();
        }
        bank.planet1(ddvalues[0], planets[0].getPop());
        bank.planet2(ddvalues[1], planets[1].getPop());
        bank.planet3(ddvalues[2], planets[2].getPop());
        ships[0].endTurn();
        ships[1].endTurn();
        et1.foo();
        turn += 1;
        texts[4].changeText("Turn: " + turn);
        if(selected_ship != null){
            texts[3].changeText("Moves remaining: " + selected_ship.moves);
        } else if(selected_planet != null){
            texts[2].changeText(bank.getResources());
            texts[3].changeText("Population: " + selected_planet.getPop());
        }
    }

    void fix(){
        spaces[1].setOccupied();
        spaces[40].setOccupied();
        spaces[11].setOccupied();
        spaces[23].setOccupied();
        spaces[58].setOccupied();
        texts[4].changeText("Turn: 1");
    }

    void Update(){

        if(once){
            once = false;
            fix();
            fleet[0] = enSpawner.makeShip(spaces[20], 1);
            fleet[1] = enSpawner.makeShip(spaces[60], 2);
            eneNum = 2;
        }
        
        if(Input.GetMouseButtonDown(1)){
            for(int i=0; i<3; i++){
                productions[i].SetActive(false);
                texts[i].changeText("Nothing Selected.");
            }
            texts[3].changeText("Nothing Selected.");
            selected_ship = null;
            selected_planet = null;
            Select();  
            highlight.transform.position = outB;
        }
        
        if(Input.GetMouseButtonDown(0)){
            //texts[2].changeText("Flag");
            if(selected_ship != null){
                for(int i = 0; i < 61; i++){
                    if(spaces[i].clicked){
                        if(selected_ship.movement(spaces[i])){
                            highlight.transform.position = selected_ship.getPos();
                            texts[3].changeText("Moves remaining: " + selected_ship.moves);
                            for(int j=0;j<eneNum;j++){
                                if(spaces[i].x == fleet[j].x && spaces[i].y == fleet[j].y){
                                    arena.battleStart(cam, selected_ship, fleet[j]);
                                }
                            }
                        }
                        spaces[i].foo();
                        break;
                    }
                }
            }

            if(selected_ship == null && selected_planet == null){
                for(int i = 0; i < shipNum; i++){
                    if(ships[i].clicked){
                        ships[i].foo();
                        selected_ship = ships[i];
                        highlight.transform.position = selected_ship.getPos();
                        texts[3].changeText("Moves remaining: " + selected_ship.moves);
                        texts[0].changeText("HMSS " + selected_ship.title + " selected.");
                        texts[2].changeText(selected_ship.desc);
                        texts[1].changeText(selected_ship.phil);
                        Select();
                        break;
                    }
                }

                for(int i=0;i<3;i++){
                    if(planets[i].clicked){
                        productions[i].SetActive(true);
                        planets[i].foo();
                        selected_planet = planets[i];
                        highlight.transform.position = selected_planet.getPos();
                        texts[1].changeText(selected_planet.desc);
                        texts[0].changeText("Planet " + selected_planet.title + " selected.");
                        texts[2].changeText(bank.getResources());
                        texts[3].changeText("Population: " + selected_planet.getPop());
                        Select();
                        break;
                    }
                }
            }

            if(et1.clicked){
                EndTurn();
            }
        }
    } 
}
