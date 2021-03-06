﻿using UnityEngine;
using System.Collections;

public class TrainWreck : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	void OnTriggerEnter (Collider other)
    {
		//Trains die on hitting arena walls
		if (other.GetComponent<Collider>().tag == "Arena")
        {
			this.gameObject.transform.parent.GetComponent<SnekHead>().KillTrain();
        }

		//trains die on hitting another train
		if (other.GetComponent<Collider> ().tag == "TrainPiece") 
		{
			if(this.gameObject.transform.parent.GetComponent<SnekHead>().crazy != true)
			{
				this.gameObject.transform.parent.GetComponent<SnekHead>().KillTrain();
			}
		}

		//Trains pick up items

		else if (other.GetComponent<Collider>().tag == "SpeedPup")
		{
			this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
			this.gameObject.transform.parent.GetComponent<SnekHead>().setSpeedUp();
			Destroy(other.transform.parent.gameObject);
		}

		else if (other.GetComponent<Collider>().tag == "MultEatPup")
		{
			this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
			this.gameObject.transform.parent.GetComponent<SnekHead>().setMultEat();
			Destroy(other.transform.parent.gameObject);
		}

		else if (other.GetComponent<Collider>().tag == "RapidPup")
		{
			this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
			this.gameObject.transform.parent.GetComponent<SnekHead>().setRapid();
			Destroy(other.transform.parent.gameObject);
		}
		else if (other.GetComponent<Collider>().tag == "MultShotPup") {
			this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
			this.gameObject.transform.parent.GetComponent<SnekHead>().setMultShot();
			Destroy(other.transform.parent.gameObject);
		}
		else if (other.GetComponent<Collider>().tag == "MinePup") {
			this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
			this.gameObject.transform.parent.GetComponent<SnekHead>().setMine();
			Destroy(other.transform.parent.gameObject);
		}
        else if (other.GetComponent<Collider>().tag == "Item")
        {
            this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
            Destroy(other.transform.parent.gameObject);
        }


	}
}
