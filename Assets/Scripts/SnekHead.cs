using UnityEngine;
using System.Collections;


public class SnekHead : SnekPartMove
{
    public bool goEat;

	public string up;
	public string left;
	public string right;
	public string down;
	public string fire;

	public GameObject bullet;

	float curTime;
	float fireDelay = 0.1f;

    //Head reset
    public override void resetOffset()
    {
        //Eat testing
        if (goEat)
        {
            goEat = false;
            eat();
        }



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

		curTime += Time.deltaTime;
        //Key input - shoot
		if (Input.GetKey(fire)) {
			if (curTime >= fireDelay)
			{
				FireBullet();
				curTime = 0.0f;
			}
		}

		//Key input - turn
        if (Input.GetKey(up) && (dir != direction.south || nextPart == null))  //Cannot move backwards if there's more than one part
        {
            dir = direction.north;
			this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKey(right) && (dir != direction.west || nextPart == null))
        {
            dir = direction.east;
			this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        }
        else if (Input.GetKey(down) && (dir != direction.north || nextPart == null))
        {
            dir = direction.south;
			this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        else if (Input.GetKey(left) && (dir != direction.east || nextPart == null))
        {
            dir = direction.west;
			this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
        }
    }

	//Recursive kill
	public void KillTrain()
	{
		GameObject end = this.gameObject;
		while (end.GetComponent<SnekPartMove>().NextPart != null) 
        {
			end = end.GetComponent<SnekPartMove>().NextPart;
		}
		while (end.GetComponent<SnekPartMove>().PrevPart != null) 
        {
			end = end.GetComponent<SnekPartMove>().PrevPart;
			Destroy(end.GetComponent<SnekPartMove>().NextPart);
		}
		
		Application.LoadLevel(0);
	}

    public void setEat()
    {
        goEat = true;
    }

	//Spawns a bullet
	void FireBullet ()
	{
		GameObject shot = (GameObject) GameObject.Instantiate (bullet, gameObject.transform.position, gameObject.transform.rotation);
		switch (dir) {
		case direction.north:
			shot.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
			break;
		case direction.east:
			shot.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
			break;
		case direction.south:
			shot.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
			break;		
		case direction.west:
			shot.transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
			break;
		default:
			break;
		}
	}
}
