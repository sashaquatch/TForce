using UnityEngine;
using System.Collections;


public class SnekHead : SnekPartMove
{
	public string up;
	public string left;
	public string right;
	public string down;


	void Start()
	{

	}

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
        if (Input.GetKey(up) && (dir != direction.south || nextPart == null))  //Cannot move backwards if there's more than one part
        {
            dir = direction.north;
			this.gameObject.transform.GetChild(0).rotation = 0.0f;
        }
        else if (Input.GetKey(right) && (dir != direction.west || nextPart == null))
        {
            dir = direction.east;
			this.gameObject.transform.GetChild(0).rotation.y = 90.0f;
        }
        else if (Input.GetKey(down) && (dir != direction.north || nextPart == null))
        {
            dir = direction.south;
			this.gameObject.transform.GetChild(0).rotation.y = 180.0f;
        }
        else if (Input.GetKey(left) && (dir != direction.east || nextPart == null))
        {
            dir = direction.west;
			this.gameObject.transform.GetChild(0).rotation.y = 270.0f;
        }
    }
}
