using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // FOR NOW -- this will change when we have multiple levels.
        WallTilemap = GameObject.FindObjectOfType<WallTileMap>().GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: Make the setting of this private/protected/something
    static public Tilemap WallTilemap;

}
