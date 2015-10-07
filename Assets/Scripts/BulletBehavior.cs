using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		transform.position += (transform.forward * 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (transform.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Collider>().tag == "TrainPiece") {
			GameObject hit = other.gameObject.transform.parent.gameObject;
			while (hit.GetComponent<SnekPartMove>().NextPart != null) {
				hit = hit.GetComponent<SnekPartMove>().NextPart;
				Destroy(hit.GetComponent<SnekPartMove>().PrevPart);
			}
			Destroy(hit);
			Destroy(this.gameObject);
		}
		else if (other.GetComponent<Collider>().tag == "Arena") {
			Destroy(this.gameObject);
		}
	}
}
