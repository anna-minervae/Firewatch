using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileProperties : MonoBehaviour
{
    public string tileType;
    public int elevation;


    // Start is called before the first frame update
    void Start()
    {
        elevation = 0;
    }

    public void setType(string type) {
        tileType = type;
    }

    public void setElevation(int change) {
        elevation = elevation + change;
    }

    public int getElevation() {
        return elevation;
    }

    public string getVeg() {
        return tileType;
    }
}
