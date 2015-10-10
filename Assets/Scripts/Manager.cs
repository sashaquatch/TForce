using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //To use list

public class Manager : MonoBehaviour
{
    public int itemCount;                       //Number of items.
    public int powerupCount;                    //Number of powerups.
    public int xSize;                           //X size of map
    public int zSize;                           //Z size of map
    public bool easyMode;                       //Stops items from spawning on map edges

    public GameObject itemPrefab;               //Prefab for Item
    public GameObject multPrefab;               //Prefab for Multiple eat powerup
    public GameObject speePrefab;               //Prefan for Speed powerup

    private List<int[]> occupied;               //List of occupied locations
    private List<GameObject> items;             //List of existing items
    private Dictionary<GameObject, int> pups;   //Dictionary of possible powerups
    private List<GameObject> powerups;          //List of existing powerups

	// Use this for initialization
	void Start ()
    {
        items = new List<GameObject>();
        occupied = new List<int[]>(); 
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
            fillOccupiedLocations();
        }

        //While there are less than 3 items, generate a new item
        while (items.Count < itemCount)
        {
            GameObject newItem = (GameObject)Instantiate(itemPrefab);   //New item to spawn

            int[] coor = getClearLocation();
            
            newItem.GetComponent<Item>().xPos = coor[0];
            newItem.GetComponent<Item>().zPos = coor[1];

            occupied.Add(new int[2]);
            occupied[occupied.Count - 1][0] = coor[0];
            occupied[occupied.Count - 1][1] = coor[1];

            newItem.transform.position = new Vector3(   //Fix for spawn flash
                newItem.GetComponent<Item>().xPos,
                .5f, 
                newItem.GetComponent<Item>().zPos);
            items.Add(newItem); //Add new item
        }
    }

    //Fills a list of occupied locations
    private void fillOccupiedLocations()
    {
        occupied = new List<int[]>();
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject)); 
        for (int i = 0; i < allObjects.Length; i++)
        {
            //If object is a snake part, add its position to occupied
            if(allObjects[i].GetComponent<SnekPartMove>() != null)
            {
                occupied.Add(new int[2]);
                occupied[occupied.Count - 1][0] = allObjects[i].GetComponent<SnekPartMove>().xPos;
                occupied[occupied.Count - 1][1] = allObjects[i].GetComponent<SnekPartMove>().zPos;
            }

            //If object is a snake head, add its position to occupied
            else if (allObjects[i].GetComponent<SnekHead>() != null)
            {
                occupied.Add(new int[2]);
                occupied[occupied.Count - 1][0] = allObjects[i].GetComponent<SnekHead>().xPos;
                occupied[occupied.Count - 1][1] = allObjects[i].GetComponent<SnekHead>().zPos;
            }

            //If object is an item, add its position to occupied
            else if (allObjects[i].GetComponent<Item>() != null)
            {
                occupied.Add(new int[2]);
                occupied[occupied.Count - 1][0] = allObjects[i].GetComponent<Item>().xPos;
                occupied[occupied.Count - 1][1] = allObjects[i].GetComponent<Item>().zPos;
            }
        }
    }
      
    //Gets an unoccupied location on the grid
    private int[] getClearLocation()
    {
        //New coordinates
        int[] coor = new int[2];

        //True if spawn location is occupied
        bool isOccupied = true;

        //Loop spawn occupation check
        while (isOccupied)
        {
            isOccupied = false;

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
            for (int i = 0; i < occupied.Count; i++)
            {
                if (
                    coor[0] == occupied[i][0] &&
                    coor[1] == occupied[i][1])
                {
                    isOccupied = true;
                    break;
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
