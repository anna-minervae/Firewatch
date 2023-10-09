using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class beginGame : MonoBehaviour
{
    //[SerializeField] private 
    public GameObject[,] tileMatrix = new GameObject[20, 11];
    public GameObject unusableTile;
    public GameObject emptyTile;
    public GameObject vegControl;
    private float tileX;
    private int tileY;

    private int upperX;
    private int upperY;

    public GameObject vegSetter;

    public GameObject coastal;
    public GameObject desert;
    public GameObject dryForest;
    public GameObject grassland;
    public GameObject urban;
    public GameObject water;
    public GameObject wetForest;
    public GameObject woodlands;

    // Start is called before the first frame update
    void Start()
    {
        tileX = 218.6998f;
        tileY = 420;

        for (int x = 0; x < 20; ++x) {
           for (int y = 0; y < 11; ++y)
           {
               tileMatrix[x,y] = (GameObject)Instantiate(unusableTile, new Vector3(tileX,tileY,0), Quaternion.identity);
               tileY = tileY - 35;
               //Debug.Log("Tile: " + x + ", " + y);
           }
           tileX = tileX + 35;
           tileY = 420;
        }

        upperX = 0;
        upperY = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTiles(int length, int width) {
        tileX = 218.6998f;
        tileY = 420;

        upperX = length;
        upperY = width;

        for (int x = 0; x < 20; ++x) {
           for (int y = 0; y < 11; ++y)
           {
                Destroy(tileMatrix[x,y]);
                if (x < length && y < width) {
                    tileMatrix[x,y] = (GameObject)Instantiate(emptyTile, new Vector3(tileX,tileY,0), Quaternion.identity);
                    vegControl.GetComponent<DesignTiles>().setVegArray(x, y, "empty");
                }
                else {
                    tileMatrix[x,y] = (GameObject)Instantiate(unusableTile, new Vector3(tileX,tileY,0), Quaternion.identity);
                    vegControl.GetComponent<DesignTiles>().setVegArray(x, y, "unusable");
                }
               tileY = tileY - 35;
               //Debug.Log("Tile: " + x + ", " + y);
           }
           tileX = tileX + 35;
           tileY = 420;
        }
    }

    public void setTilesVeg(int x, int y, string newType) {
        if (x < 20 && x > -1 && y < 11 && y > -1) {
            if (!string.Equals(vegControl.GetComponent<DesignTiles>().getTileVeg(x,y), "unusable")) {
                string vegType = newType;
                Destroy(tileMatrix[x,y]);
                if (string.Equals(newType, "coastal")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(coastal, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else if (string.Equals(newType, "desert")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(desert, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0 ), Quaternion.identity);
                
                } else if (string.Equals(newType, "dryforest")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(dryForest, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else if (string.Equals(newType, "grassland")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(grassland, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else if (string.Equals(newType, "urban")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(urban, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else if (string.Equals(newType, "water")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(water, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else if (string.Equals(newType, "wetforest")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(wetForest, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else if (string.Equals(newType, "woodlands")) {
                    tileMatrix[x,y] = (GameObject)Instantiate(woodlands, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                
                } else {
                    vegType = "empty";
                    tileMatrix[x,y] = (GameObject)Instantiate(emptyTile, new Vector3((float)(218.6998f + 35 * x), (float)(420 - 35 * y),0), Quaternion.identity);
                } 
                
                vegControl.GetComponent<DesignTiles>().setVegArray(x, y, newType);
                
            }
        }
    }
    
    public int findTileX(float newX) {
        int x = -1;
        
        x = (int)((newX - 218.6998f)/35);
        Debug.Log(x);
        if (((x < 19) && newX > ((tileMatrix[x, 0].transform.position.x) + 15)) && (newX > ((tileMatrix[(x + 1), 0].transform.position.x) - 15))) {
            x = x + 1;
            Debug.Log("Updated: " + x);
        }
    
        Debug.Log("X: " + x);
        return x;
    }

    public int findTileY(float newY) {
        int y = -1;
        
        y = (int)((420 - newY)/35);
        if (((y < 11) && newY < ((tileMatrix[0, y].transform.position.y) - 15)) && (newY < ((tileMatrix[0, (y + 1)].transform.position.y) + 15))) {
            y = y + 1;
            Debug.Log("Updated: " + y);
        }
    
        Debug.Log("Y: " + y);
        return y;
    }

    public GameObject getTile(int x, int y) {
        Debug.Log("getTile called with " + x + ", " + y);
        return tileMatrix[(Mathf.Max(Mathf.Min(x, 19), 0)), (Mathf.Max(Mathf.Min(y, 10), 0))];
    }

    public int getUpperX() {
        return upperX;
    }

    public int getUpperY() {
        return upperY;
    }
    
}
