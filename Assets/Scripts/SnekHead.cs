using UnityEngine;
using System.Collections;


public class SnekHead : SnekPartMove
{
    public bool goEat;
	public bool speedUp;
	public bool multEat;
	public bool rapid;

	public string up;
	public string left;
	public string right;
	public string down;
	public string fire;

	public GameObject bullet;

	float curTime;
	public float fireDelay = 0.1f;

    //Head reset
    public override void resetOffset()
    {


        //Eat now takes an int to set variable that determines the car's associated powerup
		//0 = nothing 1 = speed boost 2 = slow down 3 = bullet spread 4 = rapid fire 5 = shield cart 6 = mine cart 7 = crazy train
        if (goEat)
        {
            goEat = false;

			if(speedUp)
			{
				eat(1);
			}
			else if (rapid) {
				eat (4);
			}
			else{
            	eat(0);
			}
        }

		//boosts speed of train
		if(speedUp)
		{
			speedUp = false;
			//loops through train and boosts speed of each car
			GameObject end = this.gameObject;
			end.GetComponent<SnekPartMove>().speed +=1;
			while (end.GetComponent<SnekPartMove>().NextPart != null) 
			{
				end = end.GetComponent<SnekPartMove>().NextPart;
				end.GetComponent<SnekPartMove>().speed +=1;
				
			}
		}

		//gives train a second piece from pUp
		if (multEat)
		{
			multEat = false;
			eat(0);
		}

		//Decreases delay between shots
		if (rapid) {
			rapid = false;
			fireDelay = fireDelay / 2.0f;
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

	public void setSpeedUp()
	{
		speedUp = true;
	}

	public void setMultEat()
	{
		multEat = true;
	}

	public void setRapid()
	{
		rapid = true;
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
