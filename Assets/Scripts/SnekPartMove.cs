using UnityEngine;
using System.Collections;

public class SnekPartMove : MonoBehaviour
{
    //Enum of 90-degree directions
    public enum direction
    {
        north,
        east,
        south,
        west
    }

    //The next snake part (going backwards from head)
    protected GameObject nextPart;
    public GameObject NextPart
    {
        get { return nextPart; }
    }

    //The previous snake part (going forwards from tail)
    protected GameObject prevPart;
    public GameObject PrevPart
    {
        get { return prevPart; }
        set { prevPart = value; }
    }

    public direction dir;   //Enum direction the snake is going in.

    public int xPos;        //Integer x-position of component
    public int zPos;        //Integer y-position of component

    //0-1 offset of component
    protected float offset;
    public float Offset
    {
        get{ return offset; }
    }

	// Use this for initialization
	void Start ()
    {
        offset = 0;
        transform.position = new Vector3(xPos, .5f, zPos);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If offset is greater than 1 (meaning when the part reached the next grid space), reset back to 0 and peform head reset sequence.
        offset += 1f * Time.deltaTime;
        if(offset > 1)
        {
            offset = 0;
            resetOffset();
        }

        //Part is at its grid position.
        transform.position = new Vector3(xPos, .5f, zPos);

        //Part is offset in its grid position
	    switch(dir)
        {
            case direction.north:
                transform.Translate(0, 0, offset);
                break;
            case direction.east:
                transform.Translate(offset, 0, 0);
                break;
            case direction.south:
                transform.Translate(0, 0, -offset);
                break;
            case direction.west:
                transform.Translate(-offset, 0, 0);
                break;
        }
    }

    //Go from head-to-tail, trigger position passing if tail.
    public void startFromLast()
    {
        if (nextPart == null)   //True when tail
        {
            posPass();
        }
        else
        {
            nextPart.GetComponent<SnekPartMove>().startFromLast();
        }
    }

    //Position passing, go from tail-to-head and get the direction and position of the next snake part.
    public void posPass()
    {
        if(prevPart != null)    //True when not head
        {
            dir = prevPart.GetComponent<SnekPartMove>().dir;
            xPos = prevPart.GetComponent<SnekPartMove>().xPos;
            zPos = prevPart.GetComponent<SnekPartMove>().zPos;

            prevPart.GetComponent<SnekPartMove>().posPass();
        }
    }

    //Does nothing for any non-head parts.
    public virtual void resetOffset()
    {

    }

    //Generate a new snake part as the new tail
    public void eat()
    {
        if(nextPart == null)    //True when tail
        {
            nextPart = (GameObject)Instantiate(Resources.Load("SnekPart"));
            nextPart.GetComponent<SnekPartMove>().PrevPart = transform.gameObject;
            nextPart.GetComponent<SnekPartMove>().dir = dir;
            nextPart.GetComponent<SnekPartMove>().xPos = xPos;
            nextPart.GetComponent<SnekPartMove>().zPos = zPos;
        }
        else
        {
            nextPart.GetComponent<SnekPartMove>().eat();
        }
        
    }
}
