using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class getInput : MonoBehaviour
{
    private int value;
    public GameObject inputBox;

    // Start is called before the first frame update
    void Start()
    {
        value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string input){
        //string input = inputBox.GetComponent<Text>().text.ToString();
        Debug.Log("Unparsed: " + input);
        
        if (Int32.TryParse(input, out int i))
        {
            value = i;
        }
        else
        {
            value = 0;
            Debug.Log("Can't parse");
        }
        Debug.Log("Input value: " + value);
    }

    public int getValue(){
        return value;
    }
}
