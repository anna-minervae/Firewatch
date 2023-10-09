using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class getInputString : MonoBehaviour
{
    private string value;
    public GameObject inputBox;

    // Start is called before the first frame update
    void Start()
    {
        value = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string input){
        value = input;
    }

    public string getValue(){
        return value;
    }
}
