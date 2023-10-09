using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fireButton : MonoBehaviour
{
    int startX;
    int startY;
    public GameObject fireSetter;

    public void ButtonWasClicked() {
        
        if (fireSetter.GetComponent<StartFire>().getSelectingStart()) {
            Debug.Log("Let it burn");
            fireSetter.GetComponent<StartFire>().setSelectingStart(false);
            //fireSetter.GetComponent<StartFire>().setFire(true);
            fireSetter.GetComponent<StartFire>().letItBurn();
            Debug.Log(fireSetter.GetComponent<StartFire>().soundTheAlarm());
        } else if (!fireSetter.GetComponent<StartFire>().soundTheAlarm()) {
            fireSetter.GetComponent<StartFire>().selectStart();
            Debug.Log("Starting a fire");
        }
    }
}
