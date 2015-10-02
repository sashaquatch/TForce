using UnityEngine;
using System.Collections;

public class SnekHead : SnekPartMove
{
    //Head reset
    public override void resetOffset()
    {
        //Eat testing
        if (Input.GetKey("q"))
            eat();

        //Go to the last snake part and trigger pos-passing
        startFromLast();

        //Change position based on direction faced
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

        //Key input
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
