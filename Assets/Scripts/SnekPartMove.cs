using UnityEngine;
using System.Collections;

public class SnekPartMove : MonoBehaviour
{
    public enum direction
    {
        north,
        east,
        south,
        west
    }

    protected GameObject nextPart;
    public GameObject NextPart
    {
        get { return nextPart; }
    }

    protected GameObject prevPart;
    public GameObject PrevPart
    {
        get { return prevPart; }
        set { prevPart = value; }
    }

    public direction dir;
    public direction preDir;

    public int xPos;
    public int zPos;

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
        offset += 1f * Time.deltaTime;
        if(offset > 1)
        {
            offset = 0;
            resetOffset();
        }

        transform.position = new Vector3(xPos, .5f, zPos);

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

    public void startFromLast()
    {
        if (nextPart == null)
        {
            getNextDirPos();
        }
        else
        {
            nextPart.GetComponent<SnekPartMove>().startFromLast();
        }
    }

    public void getNextDirPos()
    {
        if(prevPart != null)
        {
            dir = prevPart.GetComponent<SnekPartMove>().dir;
            xPos = prevPart.GetComponent<SnekPartMove>().xPos;
            zPos = prevPart.GetComponent<SnekPartMove>().zPos;

            prevPart.GetComponent<SnekPartMove>().getNextDirPos();
        }
    }

    public virtual void resetOffset()
    {

    }

    public void eat()
    {
        
        if(nextPart == null)
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
