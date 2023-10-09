using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateTiles : MonoBehaviour
{
    public GameObject fireSetter;
    private bool settingElevation;
    private string direction;
    public GameObject iconHighlight;
    public GameObject tileBoard;
    public GameObject vegSetter;
    private int[,] elevationArray  = new int[20,11];

    private GameObject[,] elevColorArray  = new GameObject[20,11];
    public GameObject baseElevColor;
    public GameObject lowestElevColor;
    public GameObject lowElevColor;
    public GameObject highElevColor;
    public GameObject highestElevColor;
    public GameObject upArrow;
    public GameObject downArrow;
    
    public bool displayingElev;
    public bool elevReset;
    
    void Start()
    {
        settingElevation = false;
        displayingElev = false;
        elevReset = false;

        direction = "";
 
        for (int x = 0; x < 20; x++) {
            for (int y = 0; y < 11; y++) {
                elevationArray[x,y] = 0;
                elevColorArray[x,y] = (GameObject)Instantiate(baseElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("click" + Input.mousePosition.x + ", " + Input.mousePosition.y);
            if (!fireSetter.GetComponent<StartFire>().soundTheAlarm()) {
                Vector3 mousePos = Input.mousePosition;
                if (mousePos.x > 810 && mousePos.x < 880 && mousePos.y > 460 && mousePos.y < 490 ) {
                    vegSetter.GetComponent<DesignTiles>().setIsDesigning(false);
                    settingElevation = true;
                    displayElev();

                    if (settingElevation) {
                        string oldDirection = direction;
                        upOrDown(mousePos.x);
                        if (string.Equals(oldDirection, direction)) {
                            settingElevation = false;
                        }
                    }
                } else if (mousePos.x > 549 && mousePos.x < 800 && mousePos.y > 444 && mousePos.y < 495) {
                    settingElevation = true;
                }


                if (settingElevation && (mousePos.x > 200) && (mousePos.y < 440)) {
                    Debug.Log("Changing elevation");

                    int tileX = tileBoard.GetComponent<beginGame>().findTileX(mousePos.x);
                    int tileY = tileBoard.GetComponent<beginGame>().findTileY(mousePos.y);

                    if (tileX < tileBoard.GetComponent<beginGame>().getUpperX() && tileY < tileBoard.GetComponent<beginGame>().getUpperY()) {
                        setElevArray(tileX, tileY);
                    }
                }
            }
        }
        if (!settingElevation && iconHighlight.transform.position.y < 599) {
            iconHighlight.GetComponent<MoveHighlight>().moveHere(600, 600);
        }
        if (settingElevation) {
            if (!displayingElev) {
                displayElev();
            }
            vegSetter.GetComponent<DesignTiles>().setIsDesigning(false);
            
        } else {
            resetElevationColors();
            displayingElev = false;
            direction = "";
        }

        if (vegSetter.GetComponent<DesignTiles>().isDesigning()) {
            iconHighlight.GetComponent<MoveHighlight>().moveHere(600, 600);
        }
    }


    public void resetElevationArray(){
        //elevationArray = new int[20, 11];
        if (!elevReset) {
            for (int x = 0; x < 20; ++x) {
                for (int y = 0; y < 11; ++y) {
                    elevationArray[x,y] = 0;
                    resetElevationColors();
                }
        }
        }
    }

    public void resetElevationColors(){
        if (!elevReset) {
            for (int x = 0; x < 20; ++x) {
                for (int y = 0; y < 11; ++y) {
                    elevColorArray[x,y] = (GameObject)Instantiate(baseElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Destroy(elevColorArray[x,y]);
                    Debug.Log(x + ", " + y + " destroyed (in resetElevationColors)");
                }
            }

            GameObject[] allElevTiles = GameObject.FindGameObjectsWithTag("elevationMarker");
                foreach(GameObject tile in allElevTiles) {
                    Destroy(tile);
                }
        }
        elevReset = true;
    }

    public void upOrDown(float x) {
        if (x > 810 && x < 845) {
            direction = "up";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(upArrow.transform.position.x, upArrow.transform.position.y);
        } else if (x < 880) {
            direction = "down";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(downArrow.transform.position.x, downArrow.transform.position.y);
        }
    }

    public void setElevArray(int x, int y){
        if (string.Equals(direction, "up")) {
            elevationArray[x,y] = elevationArray[x,y] + 10;
        } else if (string.Equals(direction, "down")) {
            elevationArray[x,y] = elevationArray[x,y] - 10;
        }
        Debug.Log("Elevation of " + x + ", " + y + " = " + elevationArray[x,y]);
        displayElev();
    }

    public void resetElev(int x, int y){
        elevationArray[x,y] = 0;
        Destroy(elevColorArray[x,y]);
    }

    public int getTileElev(int x, int y) {
        return elevationArray[x, y];
    }
    
    public bool isSettingElev() {
        return settingElevation;
    }

    public void displayElev() {
        Debug.Log("Displaying Elev");

        resetElevationColors();
        for (int x = 0; x < tileBoard.GetComponent<beginGame>().getUpperX(); x++) {
            for (int y = 0; y < tileBoard.GetComponent<beginGame>().getUpperY(); y++) {
                //elevColorArray[x,y] = (GameObject)Instantiate(baseElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);

                Destroy(elevColorArray[x,y]);
                Debug.Log(x + ", " + y + " destroyed");

                if (elevationArray[x, y] > 0 && elevationArray[x, y] < 30) {
                    elevColorArray[x, y] = (GameObject)Instantiate(highElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Debug.Log(x + ", " + y + ": " + elevationArray[x, y]);
                
                } else if (elevationArray[x, y] >= 30) {
                    elevColorArray[x, y] = (GameObject)Instantiate(highestElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Debug.Log(x + ", " + y + ": " + elevationArray[x, y]);
                
                } else if (elevationArray[x, y] < 0 && elevationArray[x, y] > -30) {
                    elevColorArray[x, y] = (GameObject)Instantiate(lowElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Debug.Log(x + ", " + y + ": " + elevationArray[x, y]);
                
                } else if (elevationArray[x, y] <= -30) {
                    elevColorArray[x, y] = (GameObject)Instantiate(lowestElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Debug.Log(x + ", " + y + ": " + elevationArray[x, y]);
                
                }  else {
                    //elevColorArray[x, y] = (GameObject)Instantiate(baseElevColor, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Destroy(elevColorArray[x,y]);
                }
            }
        }
        elevReset = false;
        displayingElev = true;
    }

    public void setSettingElev (bool isSetting){
        settingElevation = isSetting;
    }
}
