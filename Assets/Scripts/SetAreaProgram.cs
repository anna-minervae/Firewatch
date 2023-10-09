using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAreaProgram : MonoBehaviour
{
    int newXLength;
    int newYWidth;
    public GameObject xInput;
    public GameObject yInput;
    public GameObject tileSetter;
    public GameObject fireSetter;
    public GameObject tileDesigner; //connect to resetVegArray
    public GameObject elevSetter;

    public void ButtonWasClicked() {
        if (!fireSetter.GetComponent<StartFire>().soundTheAlarm()) {
            Debug.Log("Set Area pressed");
            Debug.Log("X-Input: " + xInput.GetComponent<getInput>().getValue());
            Debug.Log("Y-Input: " + yInput.GetComponent<getInput>().getValue());
            newXLength = xInput.GetComponent<getInput>().getValue(); //error on this line
            newYWidth = yInput.GetComponent<getInput>().getValue();
            if (newXLength > 0 && newYWidth > 0) {
                tileSetter.GetComponent<beginGame>().setTiles(newXLength, newYWidth);
            }

            elevSetter.GetComponent<ElevateTiles>().resetElevationArray();
            elevSetter.GetComponent<ElevateTiles>().resetElevationColors();
            tileDesigner.GetComponent<DesignTiles>().resetVegetationArray();
        }
    }
}
