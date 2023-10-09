using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFire : MonoBehaviour
{
    private bool isThereFire;
    public GameObject tileControl;
    public GameObject vegControl;
    public GameObject elevControl;

    public GameObject fire;
    public GameObject fireHighlight;
    public GameObject fireBG;
    public bool[,] tileOnFire = new bool[20, 11];
    public bool[,] toSetOnFire = new bool[20, 11];

    private bool boardFull = true;
    private bool isSelectingStart = false;

    private bool isBurning;

    private int tileX;
    private int tileY;
    private string windDirection;

    public GameObject windInput;
    public GameObject precipInput;
    public GameObject tempInput;
    public GameObject windDropdown;

    // Start is called before the first frame update
    void Start()
    {
        isThereFire = false;
        tileX = 0;
        tileY = 0;

        isBurning = false;

        for (int x = 0; x < 20; x++) {
            for (int y = 0; x < 11; y++) {
                tileOnFire[x,y] = false;
                toSetOnFire[x,y] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isThereFire) {
            vegControl.GetComponent<DesignTiles>().setIsDesigning(false);
            elevControl.GetComponent<ElevateTiles>().setSettingElev(false);
            fireBG.GetComponent<Renderer>().enabled = true;
        } else {
            fireBG.GetComponent<Renderer>().enabled = false;
        }


        if (Input.GetMouseButtonDown(0)) {
            if (isSelectingStart) {
                fireHighlight.GetComponent<MoveHighlight>().moveHere(tileControl.GetComponent<beginGame>().getTile(0, 0).transform.position.x, tileControl.GetComponent<beginGame>().getTile(0, 0).transform.position.y);
                Debug.Log("Choose a tile for fire");
                    fireHighlight.GetComponent<Renderer>().enabled = true;
                    Vector3 mousePos = Input.mousePosition;
                    if ((mousePos.x > 200) && (mousePos.y < 440)) {
                        tileX = tileControl.GetComponent<beginGame>().findTileX(mousePos.x);
                        tileY = tileControl.GetComponent<beginGame>().findTileY(mousePos.y);
                        fireHighlight.GetComponent<MoveHighlight>().moveHere(tileControl.GetComponent<beginGame>().getTile(tileX, tileY).transform.position.x, tileControl.GetComponent<beginGame>().getTile(tileX, tileY).transform.position.y);
                        Debug.Log("Fire starting tile: " + tileX + ", " + tileY);
                    }
            }   
        }

        if (Input.GetKeyDown("space"))
        {
            if (isThereFire) {
                Debug.Log("Advancing fire");
                bushfire();
                combineArrays();
            }
        }

        if (isThereFire && !isSelectingStart) {
            fireHighlight.GetComponent<Renderer>().enabled = false;
        }
        
    }

    public void letItBurn() {
        Debug.Log("Let it burn for real");
        isSelectingStart = false;
        isThereFire = true;
        fireHighlight.GetComponent<MoveHighlight>().moveHere(1000, 1000);
        tileOnFire[tileX, tileY] = true;
        displayFlames();
    }

    public IEnumerator burning() {
        Debug.Log("waiting");
        isBurning = true;
        yield return new WaitForSecondsRealtime(3);
        bushfire();
        combineArrays();
        Debug.Log("done waiting");
        isBurning = false;
    }

    public void bushfire() {
        windDirection = "";
        int upperLimitX = (tileControl.GetComponent<beginGame>().getUpperX());
        int upperLimitY = (tileControl.GetComponent<beginGame>().getUpperY());

        for (int x = 0; x < upperLimitX; x++) {
            for (int y = 0; y < upperLimitY; y++) {
                
                if (tileOnFire[x, y]) {
                    
                    genProbability(x, Mathf.Max(y - 1, 0), x, y, "North");

                    genProbability(Mathf.Min(x + 1, upperLimitX), Mathf.Max(y - 1, 0), x, y, "Northeast");

                    genProbability(Mathf.Min(x + 1, upperLimitX), y, x, y, "East");

                    genProbability(Mathf.Min(x + 1, upperLimitX), Mathf.Min(y + 1, upperLimitY), x, y, "Southeast");

                    genProbability(x, Mathf.Min(y + 1, upperLimitY), x, y, "South");  

                    genProbability(Mathf.Max(x - 1, 0), y + 1, x, y, "Southwest");

                    genProbability(Mathf.Max(x - 1, 0), y, x, y, "West");

                    genProbability(Mathf.Max(x - 1, 0), Mathf.Max(y - 1, 0), x, y, "Northwest");
                    
                }

            }
        }
        displayFlames();
    }

    public void displayFlames(){
        GameObject[] allFlames = GameObject.FindGameObjectsWithTag("fire");
        foreach(GameObject flame in allFlames) {
            Destroy(flame);
        }

        int upperLimitX = (tileControl.GetComponent<beginGame>().getUpperX());
        int upperLimitY = (tileControl.GetComponent<beginGame>().getUpperY());

        for (int x = 0; x < upperLimitX; x++) {
            for (int y = 0; y < upperLimitY; y++) {
                if (tileOnFire[x,y]) {
                    Debug.Log("Displaying flames at " + x + ", " + y);
                    Instantiate(fire, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                    Debug.Log("flame instantiated");
                }
            }
        }
    }

    public void genProbability(int x, int y, int firstX, int firstY, string relDirection) {
        int prob = 0;
        string vegType = vegControl.GetComponent<DesignTiles>().getTileVeg(x, y);
        int rainfall = precipInput.GetComponent<getInput>().getValue();
        int temperature = tempInput.GetComponent<getInput>().getValue();
        int windSpeed = windInput.GetComponent<getInput>().getValue();
        windDirection = windDropdown.GetComponent<getInputString>().getValue();
        //need to figure out topography--second check

        //get all values
        if (!tileOnFire[x, y]) {
            //VEGETATION
            if (vegType.Equals("coastal")) {
                prob += 2;
            } else if (vegType.Equals("desert")) {
                prob += 10;
            } else if (vegType.Equals("dryForest")) {
                prob += 45;
            } else if (vegType.Equals("grassland")) {
                prob += 30;
            } else if (vegType.Equals("urban")) {
                prob += 35;
            } else if (vegType.Equals("wetForest")) {
                if (rainfall < 15) {
                    prob += 30;
                } else {
                    prob += 15;
                }
            } else if (vegType.Equals("woodlands")) {
                prob += 40;
            }

            //RAINFALL 
            if (rainfall < 15) {
                prob += 15;
            } else if (rainfall < 20) {
                prob += 10;
            } else if (rainfall < 30) {
                prob += 8;
            } else if (rainfall <= 40) {
                prob += 5;
            } else if (rainfall < 50) {
                prob -= 20;
            } else {
                prob -= 25;
            }

            //WIND
            if (windDirection.Equals(relDirection)) {
                prob += 15;
            } else if (windDirection.ToLower().Contains(relDirection.ToLower()) || relDirection.ToLower().Contains(windDirection.ToLower())) {
                prob += 10;
            }
            if (windSpeed > 10 && windSpeed < 30) {
                prob += 15;
            } else if (windSpeed > 30) {
                prob -= 15;
            }

            
            //TEMPERATURE
            if (temperature > 20) {
                prob += (int)(temperature/2);
            } else if (temperature < 7) {
                prob -= 5;
            }

            //ELEVATION
            int elevDiff = elevControl.GetComponent<ElevateTiles>().getTileElev(x, y) - elevControl.GetComponent<ElevateTiles>().getTileElev(firstX, firstY);
            if (elevDiff > 0) { //If x/y > firstX/firstY
                prob += ((int)(elevDiff/10)) * 5;

            } else if (elevDiff < 0) {
                prob -= ((int)(elevDiff/10)) * 5;
            }
        }


        if (vegType.Equals("water")) {
            prob = 0;
            tileOnFire[x, y] = false;
        }

        if (Random.Range(1, 100) < prob) {
            toSetOnFire[x, y] = true;
        } else {
            toSetOnFire[x, y] = false;
        }
    }

    public void combineArrays() {
        int upperLimitX = (tileControl.GetComponent<beginGame>().getUpperX());
        int upperLimitY = (tileControl.GetComponent<beginGame>().getUpperY());

        for (int x = 0; x < upperLimitX; x++) {
            for (int y = 0; y < upperLimitY; y++) {
                if ((tileOnFire[x,y] == false) && (toSetOnFire[x,y] == true)) {
                    tileOnFire[x,y] = true;
                    toSetOnFire[x,y] = false;
                }
            }
        }
    }

    public void selectStart() {
        boardFull = true;
        vegControl.GetComponent<DesignTiles>().setIsDesigning(false);
        elevControl.GetComponent<ElevateTiles>().setSettingElev(false);

        int upperLimitX = (tileControl.GetComponent<beginGame>().getUpperX());
        int upperLimitY = (tileControl.GetComponent<beginGame>().getUpperY());

        boardFull = (tileControl.GetComponent<beginGame>().getUpperX() > 1);
        boardFull = (tileControl.GetComponent<beginGame>().getUpperY() > 1);
        boardFull = (precipInput.GetComponent<getInput>().getValue() != null);
        boardFull = (tempInput.GetComponent<getInput>().getValue() != null);
        boardFull = (windInput.GetComponent<getInput>().getValue() != null);


        for (int x = 0; x < (upperLimitX); x++) {
            for (int y = 0; y < (upperLimitY); y++) {
                if ((vegControl.GetComponent<DesignTiles>().getTileVeg(x,y)).Equals("empty")) {
                    boardFull = false;
                    break;
                }
            }
        }

        if (boardFull) {
            Debug.Log("all checks passed");
            isSelectingStart = true;
        } else {
            setFire(false);
        }
    }

    public bool soundTheAlarm() {
        return isThereFire;
    }

    public void setFire(bool isItLit) {
        isThereFire = isItLit;
    }

    public bool getSelectingStart() {
        return isSelectingStart;
    }

    public void setSelectingStart(bool isSelecting) {
        isSelectingStart = isSelecting;
    }
}