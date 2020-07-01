using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletTileMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pelletEaters = GameObject.FindObjectsOfType<PelletEater>();

        myTileMap = GetComponent<Tilemap>();
    }

    // TODO: Add code for pellet eaters to signal updating this
    // whenever they die/respawn, if that's something that happens.
    PelletEater[] pelletEaters;

    Tilemap myTileMap;

    // What happens when we eat a pellet on this map?

    public int PelletPoints = 1;
    public bool RequiredForLevelCompletion = false;
    public float PowerSeconds = 0;

    // Update is called once per frame
    void Update()
    {
        // Is a pellet eater on a tile with a pellet?

        foreach(PelletEater pe in pelletEaters)
        {
            CheckPellet(pe);
        }
    }

    void CheckPellet(PelletEater pelletEater)
    {
        Vector2 offsetPosition = (Vector2)pelletEater.transform.position + new Vector2(0.5f, 0.5f);

        // What tile is the pellet eater "in"? 
        TileBase tile = GetTileAt(offsetPosition);

        if(tile == null)
        {
            // Empty tile with no pellets
            return;
        }

        Debug.Log("NOM");

        EatPelletAt(offsetPosition);
    }

    void EatPelletAt( Vector2 pos )
    {
        SetTileAt( pos, null );

        // TODO: Do the thing...points and whatnot.
    }

    void SetTileAt ( Vector2 pos, TileBase tile )
    {
        // Get the tile from the tilemap at position pos

        // First, we need to change the World Position, to a tile cell index
        Vector3Int cellPos = myTileMap.WorldToCell( pos );

        // Now return the actual tile!
        myTileMap.SetTile( cellPos, tile );
    }


    TileBase GetTileAt ( Vector2 pos )
    {
        // Get the tile from the tilemap at position pos

        // First, we need to change the World Position, to a tile cell index
        Vector3Int cellPos = myTileMap.WorldToCell( pos );

        // Now return the actual tile!
        return myTileMap.GetTile( cellPos );
    }


}
