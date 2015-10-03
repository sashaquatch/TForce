using UnityEngine;
using System.Collections;

public class SnekHead : SnekPartMove
{
    public override void subStart()
    {
        eating = false;
        scale = 0;
    }

    //Modify the scale and peform actions for when scale > 1
    public override void modScale()
    {
        scale += speed * Time.deltaTime;
        if (scale > 1)
        {
            //Eat testing
            if (Input.GetKey("q"))
            {
                setEating(true);
            }
            else
            {
                setEating(false);
            }

            //Key input
            if (Input.GetKey("w") && dir != direction.south)
            {
                dir = direction.north;
            }
            else if (Input.GetKey("d") && dir != direction.west)
            {
                dir = direction.east;
            }
            else if (Input.GetKey("s") && dir != direction.north)
            {
                dir = direction.south;
            }
            else if (Input.GetKey("a") && dir != direction.east)
            {
                dir = direction.west;
            }
            
            //Construct a new part between the head and its adjacent segment
            GameObject newPart = (GameObject)Instantiate(Resources.Load("SnekPart"));
            newPart.GetComponent<SnekPartMove>().PrevPart = transform.gameObject;
            newPart.GetComponent<SnekPartMove>().xPos = xPos;
            newPart.GetComponent<SnekPartMove>().zPos = zPos;
            newPart.GetComponent<SnekPartMove>().dir = dir;
            newPart.GetComponent<SnekPartMove>().speed = speed;
            newPart.transform.position = new Vector3(xPos, 0, zPos);

            if(nextPart != null)
            {
                newPart.GetComponent<SnekPartMove>().NextPart = NextPart;
                nextPart.GetComponent<SnekPartMove>().PrevPart = newPart;
            }
            nextPart = newPart;

            //Reset head scale
            scale = 0;

            //Move head forward
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
        }
    }

    //Scales or offsets the head when its scale is less than 1.
    public override void scaleOffset()
    {
        if (scale < 1)
        {
            switch (dir)
            {
                case direction.north:
                    transform.Translate(0, 0, scale / 2 - .5f);
                    transform.localScale = new Vector3(1, 1, scale);
                    break;
                case direction.east:
                    transform.Translate(scale / 2 - .5f, 0, 0);
                    transform.localScale = new Vector3(scale, 1, 1);
                    break;
                case direction.south:
                    transform.Translate(0, 0, -scale / 2 + .5f);
                    transform.localScale = new Vector3(1, 1, -scale);
                    break;
                case direction.west:
                    transform.Translate(-scale / 2 + .5f, 0, 0);
                    transform.localScale = new Vector3(-scale, 1, 1);
                    break;
            }
        }
    }
}
