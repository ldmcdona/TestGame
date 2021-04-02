using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    private int r_con, r_civ, r_org, r_med, r_mil;
    // Start is called before the first frame update
    void Start()
    {
        r_con = r_civ = r_org = r_med = r_mil = 1;
    }

    public void planet1(int i, int p){
        for(int j=0; j<p; j++){
            switch(i){
                case 0:
                    r_con++;
                    break;
                case 1:
                    r_civ++;
                    break;
                case 2:
                    r_org++;
                    break;
                case 3:
                    r_med++;
                    break;
                case 4:
                    r_mil++;
                    break;
                default:
                    break;
            }
        }
    }

    public void planet2(int i, int p){
        for(int j=0; j<p; j++){
            switch(i){
                case 0:
                    r_con++;
                    break;
                case 1:
                    r_civ++;
                    break;
                case 2:
                    r_org++;
                    break;
                default:
                    break;
            }
        }
    }

    public void planet3(int i, int p){
        for(int j=0; j<p; j++){
            switch(i){
                case 0:
                    r_con++;
                    break;
                case 1:
                    r_med++;
                    break;
                case 2:
                    r_mil++;
                    break;
                default:
                    break;
            }
        }
    }

    public string getResources(){
        string x;
        x = "Construction: " + r_con + "\nCivilian: " + r_civ + "\nOrganic: " + r_org + "\nMedical: " + r_med + "\nMilitary: " + r_mil;
        return x;
    }
}
