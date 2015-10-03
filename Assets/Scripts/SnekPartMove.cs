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
        set { nextPart = value; }
    }

    //The previous snake part (going forwards from tail)
    protected GameObject prevPart;
    public GameObject PrevPart
    {
        get { return prevPart; }
        set { prevPart = value; }
    }

    public int xPos;        //Integer x-position of part
    public int zPos;        //Integer y-position of part
    public direction dir;   //Enum direction of part
    public float speed;     //Speed of part
    protected float scale;  //Scale of part
    protected bool eating;  //True if tail is in eating state and isn't shrinking

	// Use this for initialization
	void Start ()
    {
        subStart();
	}

    //Overridable start
    public virtual void subStart()
    {
        eating = false;
        scale = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Part is at its grid position.
        transform.position = new Vector3(xPos, 0, zPos);

        modScale();     //Modifiy the scale of the part
        scaleOffset();  //Offset the part based on its scale
    }

    //Shrink or destroy the tail if eating isn't in progress.
    public virtual void modScale()
    {
        //Shrink last part.
        if (!eating && nextPart == null)
        {
            scale -= speed * Time.deltaTime;
            if (scale < .01)
            {
                Destroy(gameObject);
            }
        }
    }

    //Scales and offsets any part with scale less than 1.
    public virtual void scaleOffset()
    {
        if (scale < 1)
        {
            switch (dir)
            {
                case direction.north:
                    transform.Translate(0, 0, -scale / 2 + .5f);
                    transform.localScale = new Vector3(1, 1, scale);
                    break;
                case direction.east:
                    transform.Translate(-scale / 2 + .5f, 0, 0);
                    transform.localScale = new Vector3(scale, 1, 1);
                    break;
                case direction.south:
                    transform.Translate(0, 0, scale / 2 - .5f);
                    transform.localScale = new Vector3(1, 1, -scale);
                    break;
                case direction.west:
                    transform.Translate(scale / 2 - .5f, 0, 0);
                    transform.localScale = new Vector3(-scale, 1, 1);
                    break;
            }
        }
    }

    //Sets the eating of the tail to be true or false.
    public void setEating(bool _eating)
    {
        if (nextPart == null)    //True when tail
        {
            eating = _eating;
            if (!eating)
                scale = 1;      //Fix for head and tail not moving in sync.
        }
        else
        {
            nextPart.GetComponent<SnekPartMove>().setEating(_eating);   //Travel back to tail.
        }
    }
}
