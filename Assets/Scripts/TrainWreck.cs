using UnityEngine;
using System.Collections;

public class TrainWreck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		print ("collision");
		if (other.GetComponent<Collider>().tag == "TrainPiece") {
			print("hit");
		}
	}


}
