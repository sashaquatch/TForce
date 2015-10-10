using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		//Spawn just in front of train
		transform.position += (transform.forward * 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//Move forward at a constant rate
		transform.position += (transform.forward * speed * Time.deltaTime);
	}

	//Collides with things
	void OnTriggerEnter(Collider other)
	{
		//On train hit
		if (other.GetComponent<Collider>().tag == "TrainPiece") {

			//Recursive kill based on KillTrain
			GameObject hit = other.gameObject.transform.parent.gameObject;
			GameObject spot = hit;
			if (hit.GetComponent<SnekPartMove>().PrevPart == null) {
				hit.GetComponent<SnekHead>().KillTrain();
			}
			while (spot.GetComponent<SnekPartMove>().NextPart != null) {
				spot = spot.GetComponent<SnekPartMove>().NextPart;
			}
			while (spot != hit) {
				spot = spot.GetComponent<SnekPartMove>().PrevPart;
				Destroy(spot.GetComponent<SnekPartMove>().NextPart);
			}
			Destroy(hit);
			Destroy(this.gameObject);
		}
		//Prevent bullets from leaving arena
		else if (other.GetComponent<Collider>().tag == "Arena") {
			Destroy(this.gameObject);
		}
	}
}
