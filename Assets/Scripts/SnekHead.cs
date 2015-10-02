using UnityEngine;
using System.Collections;

public class SnekHead : SnekPartMove
{
    public override void resetOffset()
    {
        if (Input.GetKey("q"))
            eat();

        startFromLast();

        switch (dir)
        {
            case direction.north:
                zPos++;
                break;
            case direction.east:
                xPos++;
                break;
            case direction.south:
                zPos--;
                break;
            case direction.west:
                xPos--;
                break;
        }

        if (Input.GetKey("w") && dir != direction.south)
        {
            dir = direction.north;
        }
        else if (Input.GetKey("d") && dir != direction.east)
        {
            dir = direction.east;
        }
        else if (Input.GetKey("s") && dir != direction.north)
        {
            dir = direction.south;
        }
        else if (Input.GetKey("a") && dir != direction.west)
        {
            dir = direction.west;
        }
    }
}
