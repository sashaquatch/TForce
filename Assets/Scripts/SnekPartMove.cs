using UnityEngine;
using System.Collections;

public class SnekPartMove : MonoBehaviour
{
	//used to determine powerup associate with this train piece
	//0 = nothing 1 = speed boost 2 = slow down 3 = bullet spread 4 = rapid fire 5 = shield cart 6 = mine cart 7 = crazy train
	public int powerUp;

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

    public float speed;
    public direction dir;   //Enum direction the snake is going in.
    public int xPos;        //Integer x-position of component
    public int zPos;        //Integer y-position of component

    public Material color;

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
        //this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Special update for child classes
        specialUpdate();

        //If offset is greater than 1 (meaning when the part reached the next grid space), reset back to 0 and peform head reset sequence.
        offset += speed * Time.deltaTime;
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
				this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case direction.east:
                transform.Translate(offset, 0, 0);
				this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                break;
            case direction.south:
                transform.Translate(0, 0, -offset);
				this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                break;
            case direction.west:
                transform.Translate(-offset, 0, 0);
				this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                break;
        }
    }

    //Does nothing for any non-head parts.
    public virtual void specialUpdate()
    {

    }

    //Does nothing for any non-head parts.
    public virtual void resetOffset()
    {

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

    //Generate a new snake part as the new tail
    public void eat(int pUp)
    {
        if(nextPart == null)    //True when tail
        {
            nextPart = (GameObject)Instantiate(Resources.Load("CartLow"));
            nextPart.GetComponent<SnekPartMove>().PrevPart = transform.gameObject;
            nextPart.GetComponent<SnekPartMove>().color = color;
            nextPart.GetComponent<SnekPartMove>().speed = speed;
			nextPart.GetComponent<SnekPartMove>().powerUp = pUp;
            nextPart.GetComponent<SnekPartMove>().dir = dir;
            nextPart.GetComponent<SnekPartMove>().xPos = xPos;
            nextPart.GetComponent<SnekPartMove>().zPos = zPos;
        }
        else
        {
            nextPart.GetComponent<SnekPartMove>().eat(pUp);
        }
        
    }

	//0 = nothing 1 = speed boost 2 = slow down 3 = bullet spread 4 = rapid fire 5 = shield cart 6 = mine cart 7 = crazy train
	public void OnDestroy()
	{
		//lose speed boost
		if (powerUp == 1) 
		{
			GameObject otherPart = prevPart;
			while(otherPart != null)
			{
				otherPart.GetComponent<SnekPartMove>().speed -= 1;
				otherPart = otherPart.GetComponent<SnekPartMove>().PrevPart;
			}
		}

		// lose slow down
		if (powerUp == 2) 
		{
			GameObject otherPart = prevPart;
			while(otherPart != null)
			{
				otherPart.GetComponent<SnekPartMove>().speed += 1;
				otherPart = otherPart.GetComponent<SnekPartMove>().PrevPart;
			}
		}

		//lose bullet spread
		if (powerUp == 3) 
		{
			GameObject otherPart = prevPart;
			while(otherPart.GetComponent<SnekPartMove>().PrevPart != null && otherPart.GetComponent<SnekPartMove>().powerUp != 3)
			{
				otherPart = otherPart.GetComponent<SnekPartMove>().PrevPart;
			}
			if (otherPart.GetComponent<SnekPartMove>().powerUp != 3) {
				otherPart.GetComponent<SnekHead>().loseMultShot();
			}
		}

		//lose rapid fire
		if (powerUp == 4) 
		{
			GameObject otherPart = prevPart;
			while(otherPart.GetComponent<SnekPartMove>().PrevPart != null)
			{
				otherPart = otherPart.GetComponent<SnekPartMove>().PrevPart;
			}
			otherPart.GetComponent<SnekHead>().fireDelay = otherPart.GetComponent<SnekHead>().fireDelay * 2.0f;

		}
		
		//lose mines
		if (powerUp == 6) 
		{
			GameObject otherPart = prevPart;
			while(otherPart.GetComponent<SnekPartMove>().PrevPart != null && otherPart.GetComponent<SnekPartMove>().powerUp != 6)
			{
				otherPart = otherPart.GetComponent<SnekPartMove>().PrevPart;
			}
			if (otherPart.GetComponent<SnekPartMove>().powerUp != 6) {
				otherPart.GetComponent<SnekHead>().mine = false;
			}
		}

		//lose craziness?
		if (powerUp == 7) 
		{
		}
	}
}
