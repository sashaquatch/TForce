using UnityEngine;
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
		if (other.GetComponent<Collider>().tag == "TrainPiece")
        {
			this.gameObject.transform.parent.GetComponent<SnekHead>().KillTrain();
        }
        else if (other.GetComponent<Collider>().tag == "Item")
        {
            this.gameObject.transform.parent.GetComponent<SnekHead>().setEat();
            Destroy(other.transform.parent.gameObject);
        }
	}
}
