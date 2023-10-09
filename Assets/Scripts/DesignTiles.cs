using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignTiles : MonoBehaviour
{
    public GameObject fireSetter;
    public GameObject elevSetter;

    private bool settingTiles;
    private string settingTileType;
    public GameObject iconHighlight;
    public GameObject tileBoard;
    private string[,] vegetationArray  = new string[20,11];
    private string[] vegTypes = new string[8];
    private GameObject[] vegTypeIcons = new GameObject[8];

    public GameObject coastIcon;
    public GameObject desertIcon;
    public GameObject dryForIcon;
    public GameObject grassIcon;
    public GameObject urbanIcon;
    public GameObject waterIcon;
    public GameObject wetForIcon;
    public GameObject woodIcon;

    void Start()
    {
        settingTiles = false;
        settingTileType = "N/A";

        vegTypes[0] = "coastal";
        vegTypes[1] = "desert";
        vegTypes[2] = "dryForest";
        vegTypes[3] = "grassland";
        vegTypes[4] = "urban";
        vegTypes[5] = "water";
        vegTypes[6] = "wetForest";
        vegTypes[7] = "woodlands";

        vegTypeIcons[0] = coastIcon;
        vegTypeIcons[1] =  desertIcon;
        vegTypeIcons[2] =  dryForIcon;
        vegTypeIcons[3] =  grassIcon;
        vegTypeIcons[4] =  urbanIcon;
        vegTypeIcons[5] =  waterIcon;
        vegTypeIcons[6] =  wetForIcon;
        vegTypeIcons[7] =  woodIcon;

        for (int x = 0; x < 20; x++) {
            for (int y = 0; y < 11; y++) {
                vegetationArray[x,y] = "unusable";
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
                if (mousePos.x > 549 && mousePos.x < 800 && mousePos.y > 444 && mousePos.y < 495 ) {
                    settingTiles = true;

                    if (settingTiles) {
                        selectType((double)mousePos.x, settingTileType);
                        elevSetter.GetComponent<ElevateTiles>().setSettingElev(false);

                    } else {
                        iconHighlight.GetComponent<MoveHighlight>().moveHere(600, 600);
                        settingTileType = "N/A";
                    }

                    Debug.Log(settingTiles + " " + settingTileType);
                } else if (mousePos.x > 810 && mousePos.x < 880 && mousePos.y > 460 && mousePos.y < 490) {
                    settingTiles = false;
                }


                if (settingTiles && (mousePos.x > 200) && (mousePos.y < 440)) {
                    int tileX = tileBoard.GetComponent<beginGame>().findTileX(mousePos.x);
                    int tileY = tileBoard.GetComponent<beginGame>().findTileY(mousePos.y);
                    Debug.Log("COORDS: " + tileX + ", " + tileY);
                    if (tileX < tileBoard.GetComponent<beginGame>().getUpperX() && tileY < tileBoard.GetComponent<beginGame>().getUpperY()) {
                        Debug.Log("setting veg");
                        setVegArray(tileX, tileY, settingTileType);
                        tileBoard.GetComponent<beginGame>().setTilesVeg(tileX, tileY, settingTileType.ToLower());
                    }
                }
            }
        }
        if (!settingTiles && iconHighlight.transform.position.y < 599) {
            iconHighlight.GetComponent<MoveHighlight>().moveHere(600, 600);
        }
    }


    public void selectingTypeWIP(double xPos, string oldType) {
        if (xPos > 548 && xPos < 572) {
            settingTileType = "Coastal";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(562.3094, 469.5168);
        } else if (xPos > 577 && xPos < 602) {
            settingTileType = "Desert";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(592.3094, 469.5168);
        } else if (xPos > 607 && xPos < 632) {
            settingTileType = "Dry Forest";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(622.3094, 469.5168);
        } else if (xPos > 637 && xPos < 662) {
            settingTileType = "Grassland";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(652.3094, 469.5168);
        } else if (xPos > 667 && xPos < 692) {
            settingTileType = "Urban";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(682.3094, 469.5168);
        } else if (xPos > 697 && xPos < 722) {
            settingTileType = "Water";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(712.3094, 469.5168);
        } else if (xPos > 737 && xPos < 752) {
            settingTileType = "Wet Forest";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(742.3094, 469.5168);
        } else if (xPos > 757 && xPos < 782) {
            settingTileType = "Woodlands";
            iconHighlight.GetComponent<MoveHighlight>().moveHere(772.3094, 469.5168);
        } else {
            settingTiles = false;
            settingTileType = "N/A";
        }

        if (string.Equals(oldType, settingTileType)) {
            settingTiles = false;
            settingTileType = "N/A";
        }
    }

    public int selectType(double xPos, string oldType) {
        int typeNum = -1;

        //typeNum = (int)((xPos - (vegTypeIcons[0].transform.position.x))/25);

        if (xPos > (vegTypeIcons[0].transform.position.x - (18)) && xPos < (vegTypeIcons[0].transform.position.x + (8))) {
            typeNum = 0;
        } else if (xPos > (vegTypeIcons[1].transform.position.x - (18)) && xPos < (vegTypeIcons[1].transform.position.x + (8))) {
            typeNum = 1;
        } else if (xPos > (vegTypeIcons[2].transform.position.x - (18)) && xPos < (vegTypeIcons[2].transform.position.x + (8))) {
            typeNum = 2;
        } else if (xPos > (vegTypeIcons[3].transform.position.x - (18)) && xPos < (vegTypeIcons[3].transform.position.x + (8))) {
            typeNum = 3;
        } else if (xPos > (vegTypeIcons[4].transform.position.x - (18)) && xPos < (vegTypeIcons[4].transform.position.x + (8))) {
            typeNum = 4;
        } else if (xPos > (vegTypeIcons[5].transform.position.x - (18)) && xPos < (vegTypeIcons[5].transform.position.x + (8))) {
            typeNum = 5;
        } else if (xPos > (vegTypeIcons[6].transform.position.x - (18)) && xPos < (vegTypeIcons[6].transform.position.x + (8))) {
            typeNum = 6;
        } else if (xPos > (vegTypeIcons[7].transform.position.x - (18)) && xPos < (vegTypeIcons[7].transform.position.x + (8))) {
            typeNum = 7;
        } else {
            typeNum = -1;
        }

        if (typeNum >= 0) {
            settingTileType = vegTypes[typeNum];

            iconHighlight.GetComponent<MoveHighlight>().moveHere(vegTypeIcons[typeNum].transform.position.x, vegTypeIcons[typeNum].transform.position.y);

            if (string.Equals(oldType, settingTileType)) {
                settingTiles = false;
                settingTileType = "N/A";
                iconHighlight.GetComponent<MoveHighlight>().moveHere(1000, 1000);
            }

        } else {
            settingTileType = "N/A";
            settingTiles = false;
        }

        return typeNum;
    }

    public void resetVegetationArray(){
        vegetationArray = new string[20, 11];
        for (int x = 0; x < 20; ++x) {
            for (int y = 0; y < 11; ++y) {
                vegetationArray[x,y] = "empty";
            }
        }
    }

    public void setVegArray(int x, int y, string newType){
        vegetationArray[x,y] = newType;
    }

    public string getTileVeg(int x, int y) {
        return vegetationArray[x, y];
    }
    
    public bool isDesigning() {
        return settingTiles;
    }

    public void setIsDesigning(bool areYou) {
        settingTiles = areYou;
    }

    public string settingType() {
        return settingTileType;
    }
}
