using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        mazeMover = GetComponent<MazeMover>();
        mazeMover.OnEnterNewTile += OnEnterNewTile;
    }

    MazeMover mazeMover;

    public float forwardWeight = 0.5f; // Chance of continuing forward instead of turning at an intersection

    // Update is called once per frame
    void Update()
    {
        

        // mazeMover.SetDesiredDirection( newDir.normalized );

    }

    void DoTurn()
    {
        // Do a turn to the "left" or "right"

        Vector2 newDir = Vector2.zero;

        Vector2 oldDir = mazeMover.GetDesiredDirection();

        if( Mathf.Abs(oldDir.x) > 0)
        {
            // Moving left-right currently.
            // A "smarter" enemy might ask: Is the player above or below us, and
            // weight the "Y" direction accordingly
            newDir.y = Random.Range(0, 2) == 0 ? -1 : 1;
        }
        else
        {
            // Currently moving up-down
            newDir.x = Random.Range(0, 2) == 0 ? -1 : 1;
        }

        mazeMover.SetDesiredDirection( newDir );
    }

    void OnEnterNewTile()
    {
        Debug.Log( gameObject.name + " OnEnterNewTile");
        // If we're face-planting into a wall, first invert our direction
        // THEN we decide if we want to keep going "Straight" (backwards)
        // or attempt a turn
        if(mazeMover.WouldHitWall())
        {
            Vector2 newDir = mazeMover.GetDesiredDirection();
            newDir.x *= -1f;
            newDir.y *= -1f;
            mazeMover.SetDesiredDirection(newDir);
        }

        if( Random.Range(0f, 1f) < forwardWeight )
        {
            // Keep moving forward.
            return;
        }

        DoTurn();
    }
}
