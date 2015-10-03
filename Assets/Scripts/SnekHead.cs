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
        if (Input.GetKey("w") && (dir != direction.south || nextPart == null))  //Cannot move backwards if there's more than one part
        {
            dir = direction.north;
        }
        else if (Input.GetKey("d") && (dir != direction.west || nextPart == null))
        {
            dir = direction.east;
        }
        else if (Input.GetKey("s") && (dir != direction.north || nextPart == null))
        {
            dir = direction.south;
        }
        else if (Input.GetKey("a") && (dir != direction.east || nextPart == null))
        {
            dir = direction.west;
        }
    }
}
