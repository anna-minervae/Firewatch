using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class onTileClick : MonoBehaviour
{
    public GameObject designControl;
    public GameObject tileControl;

    private void OnMouseDown() {
        //Debug.Log("Tile: " + tileControl.GetComponent<beginGame>().findTileX(this.transform.x) + ", " + tileControl.GetComponent<beginGame>().findTileY(this.transform.y));
        //tileControl.GetComponent<beginGame>().setTilesVeg((tileControl.GetComponent<beginGame>().findTileX(this.transform.x)), (tileControl.GetComponent<beginGame>().findTileY(this.transform.y)), (designControl.GetComponent<DesignTiles>().settingType()));
    }
}
