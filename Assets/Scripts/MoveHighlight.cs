using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHighlight : MonoBehaviour
{
    public GameObject moveThis;

    // Start is called before the first frame update
    void Start()
    {
        moveThis.transform.position = new Vector3(600, 600, 2);
    }

    public void moveHere(double x, double y) {
        moveThis.transform.position = new Vector3((float)x, (float)y, 2);
    }
}
