using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static bool emptyCursor = true;
    public static ObjetoSeleccionable selectedObject;

    public Event[] allRegularEvents;
    public Event[] allRandomEvents;
    private Event chosenRandomEvent;
    public Event[] chosenEvents;

    public GameObject[] eventsDisplay;

    private int[] testingIndexes;

    public Event currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        chosenRandomEvent = allRandomEvents[Random.Range(0, allRandomEvents.Length)];

        testingIndexes = Select3RandomIndexes();
        chosenEvents = new Event[] { allRegularEvents[testingIndexes[0]], allRegularEvents[testingIndexes[1]], allRegularEvents[testingIndexes[2]], chosenRandomEvent };
        currentEvent = chosenEvents[0];
        for (int i = 0; i<4; i++)
        {
            eventsDisplay[i].GetComponent<SpriteRenderer>().sprite = chosenEvents[i].eventArt;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    int[] Select3RandomIndexes()
    {
        int[] indexes = new int[3];
        indexes[0] = Random.Range(0, allRegularEvents.Length);
        indexes[1] = Random.Range(0, allRegularEvents.Length);
        indexes[2] = Random.Range(0, allRegularEvents.Length);
        Debug.Log("selecting");
        Debug.Log(indexes[0]);
        Debug.Log(indexes[1]);
        Debug.Log(indexes[2]);
        if (indexes[0]== indexes[1] || indexes[0] == indexes[2] || indexes[1] == indexes[2])
        { indexes=Select3RandomIndexes(); }
        return indexes;
    }

    public void PressReady()
    {
        if(emptyCursor == true)
        {
            Debug.Log("cambiar a modo responder eventos");
        }

    }
}
