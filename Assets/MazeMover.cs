using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Test:
        desiredDirection = new Vector2( -1, 0 );

        // Set our initial target position to be our starting position
        // so that the Update will, well, update the target
        targetPos = transform.position;

        // This works as long as there is only ONE Tilemap in the scene
        wallTileMap = GameObject.FindObjectOfType<Tilemap>();
    }

    float Speed = 3;    // How many world-space "tiles" this unit moves in one second

    Vector2 desiredDirection;  // The current direction we want to move in

    Vector2 targetPos;  // Always a legal, empty tile

    Tilemap wallTileMap;

    // Update is called once per GRAPHIC frame
    // This is the best place to read inputs and do things like updating animations
    void Update()
    {
        // First, make sure we can legally move in the direction we want
        UpdateTargetPosition();

        // Do the move!
        MoveToTargetPosition();
    }

    // FixedUpdate is called once per PHYSICS ENGINE frame
    // This is the best place to update the physic's system
    // velocity if you are using it to move your object
    //void FixedUpdate()
    //{
        // Our objects are not physics-enabled rigidbodies, so
        // the physics system isn't moving us, nor are we doing
        // "real" collisions, so we don't need to stress about
        // FixedUpdate
    //}

    void UpdateTargetPosition( bool force = false)
    {
        if(force == false)
        {
            // Have we reached our target?
            float distanceToTarget = Vector3.Distance( transform.position, targetPos );
            if(distanceToTarget > 0)
            {
                // Not there yet, no need to update anything.
                return;
            }
        }

        // If we get here, it means we need a new target position;
        targetPos += desiredDirection;

        // "Normalize" the target position to a tile's position
        targetPos = FloorPosition(targetPos);

        if(IsTileEmpty(targetPos))
        {
            // Good to go!
            return;
        }

        // If we get here, it means that our target position is OCCUPIED!
        // So we aren't allowed to move. Stand still!
        targetPos = transform.position;
    }

    Vector2 FloorPosition( Vector2 pos )
    {
        // "Normalize" the target position to a tile's position
        // This might not line up right if we have a weirld offset tilemap
        // A "more robust" solution might be to lookup the tile at the new
        // targetPos, then read back that Tile's world coordinate?
        return new Vector2( Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    bool IsTileEmpty( Vector2 pos )
    {
        // Is there a tile at pos?
        return GetTileAt(pos) == null;
    }

    TileBase GetTileAt ( Vector2 pos )
    {
        // Get the tile from the tilemap at position pos

        // First, we need to change the World Position, to a tile cell index
        Vector3Int cellPos = wallTileMap.WorldToCell( pos );

        // Now return the actual tile!
        return wallTileMap.GetTile( cellPos );
    }

    void MoveToTargetPosition()
    {
        // How far can we move this frame?
        float distanceThisUpdate = Speed * Time.deltaTime;

        // And in what direction is this movement?
        // Towards our target position!
        Vector2 distToTarget = targetPos - (Vector2)transform.position;

        Vector2 movementThisUpdate = distToTarget.normalized * distanceThisUpdate;

        // What if we would be moving PAST the target?
        // We COULD change movementThisUpdate to have the same magnitude as distance to target
        if(distToTarget.SqrMagnitude() < movementThisUpdate.SqrMagnitude())
        {
            // Just set our position to the target, not a "move"
            transform.position = targetPos;
            return;
        }
        
        // Do the move!
        transform.Translate( movementThisUpdate );
    }

    public void SetDesiredDirection (Vector2 newDir)
    {
        // TODO: Sanity checks?
        // Make sure not diagonal? In THEORY, our PlayerMover/EnemyMover script already does this.

        // But we shouldn't accept a direction that would slam us into a wall

        // If we're selection a direction that would slam us into a wall,
        // this will cause us to stop -- which doesn't feel right
        Vector2 testPos = targetPos + newDir;
        if(IsTileEmpty(testPos) == false)
        {
            // Trying to move into a wall, ignore input.
            return;
        }

        //Vector2 oldDir = desiredDirection;
        desiredDirection = newDir;

        // TODO: If the input is to reverse our direction, do it instantly?
        // UpdateTargetPosition(true);


    }

    public Vector2 GetDesiredDirection()
    {
        return desiredDirection;
    }
}
