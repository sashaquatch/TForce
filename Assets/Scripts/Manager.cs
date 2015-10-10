using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //To use list

public class Manager : MonoBehaviour
{
    public int itemCount;                   //Number of items.
    public int xSize;                       //X size of map
    public int zSize;                       //Z size of map
    public bool easyMode;                   //Stops items from spawning on map edges

    public GameObject itemPrefab;
    public GameObject multPrefab;
    public GameObject speePrefab;

    private List<GameObject> items;
    private List<GameObject> snakeParts;    //Snake parts for spawning comparison
    private List<GameObject> snakeHeads;    //Snake heads for spawning comparison
    private Dictionary<GameObject, int> pups;

	// Use this for initialization
	void Start ()
    {
        items = new List<GameObject>();
        snakeParts = new List<GameObject>();
        snakeHeads = new List<GameObject>();
        pups = new Dictionary<GameObject, int>();

        pups.Add(multPrefab, 50);
        pups.Add(speePrefab, 99);

        generateItems();
    }
	
	// Update is called once per frame
	void Update ()
    {
        cleanItems();
        generateItems();
    }

    //Generate new items
    private void generateItems()
    {
        //If there less than 3 items, fill new spawning comparison lists.
        if (items.Count < itemCount)
        {
            snakeParts = new List<GameObject>();
            snakeHeads = new List<GameObject>();
            GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for(int i = 0; i < allObjects.Length; i++)
            {
                if(allObjects[i].GetComponent<SnekPartMove>() != null)
                {
                    snakeParts.Add(allObjects[i]);
                }
                if(allObjects[i].GetComponent<SnekHead>() != null)
                {
                    snakeHeads.Add(allObjects[i]);
                }
            }
        }

        //While there are less than 3 items, generate a new item
        while (items.Count < itemCount)
        {
            GameObject newItem = (GameObject)Instantiate(itemPrefab);   //New item to spawn
            bool occupied = true;

            int[] coor = getClearLocation();
            
            newItem.GetComponent<Item>().xPos = coor[0];
            newItem.GetComponent<Item>().zPos = coor[1];

            newItem.transform.position = new Vector3(   //Fix for spawn flash
                newItem.GetComponent<Item>().xPos,
                .5f,
                newItem.GetComponent<Item>().zPos);
            items.Add(newItem); //Add new item
        }
    }

    //Gets an unoccupied location on the grid
    private int[] getClearLocation()
    {
        //New coordinates
        int[] coor = new int[2];

        //True if spawn location is occupied
        bool occupied = true;

        //Loop spawn occupation check
        while (occupied)
        {
            occupied = false;

            //New location
            if (easyMode)
            {
                coor[0] = Random.Range(1, xSize - 1);
                coor[1] = Random.Range(1, zSize - 1);
            }
            else
            {
                coor[0] = Random.Range(0, xSize);
                coor[1] = Random.Range(0, zSize);
            }

            //Check if location is occupied by snake parts.
            for (int i = 0; i < snakeParts.Count; i++)
            {
                if (
                    coor[0] == snakeParts[i].GetComponent<SnekPartMove>().xPos &&
                    coor[1] == snakeParts[i].GetComponent<SnekPartMove>().zPos)
                {
                    occupied = true;
                    break;
                }
            }

            //Check if location is occupied by snake heads.
            if (occupied == false)
            {
                for (int i = 0; i < snakeHeads.Count; i++)
                {
                    if (
                        coor[0] == snakeHeads[i].GetComponent<SnekHead>().xPos &&
                        coor[1] == snakeHeads[i].GetComponent<SnekHead>().zPos)
                    {
                        occupied = true;
                        break;
                    }
                }
            }

            //Check if location is occupied by other items.
            if (occupied == false)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (
                        coor[0] == items[i].GetComponent<Item>().xPos &&
                        coor[1] == items[i].GetComponent<Item>().zPos)
                    {
                        occupied = true;
                        break;
                    }
                }
            }
        }

        return coor;
    }

    //Remove all null-missing items.
    private void cleanItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                items.RemoveAt(i);
        }
    }
}
