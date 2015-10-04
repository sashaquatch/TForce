using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public int xPos;
    public int zPos;

	// Use this for initialization
    void Update()
    {
        transform.position = new Vector3(xPos, .5f, zPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "TrainPiece")
        {
            Destroy(gameObject);
        }
    }
}
