using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour
{
    private Text tex;

    private void Start(){
        tex = this.GetComponent<Text>();
        tex.text = "Waitting.";
    }

    public void changeText(string message){
        tex.text = message;
    }
}
