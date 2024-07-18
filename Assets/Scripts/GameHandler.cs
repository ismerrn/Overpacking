using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static bool emptyCursor = true;
    public static bool answerGridIsEmpty = true;
    public static Transform answerObject;
    public static ObjetoSeleccionable selectedObject;
    public static int eventCounter = 0;

    public Event[] allRegularEvents;
    public Event[] allRandomEvents;
    private Event chosenRandomEvent;
    public static Event[] chosenEvents;

    public GameObject[] eventsDisplay;

    private int[] testingIndexes;

    private DialogueManager dialogueManager;

    public Event currentEvent;

    public GameObject drawer;
    public GameObject readyButton;
    public GameObject confirmButton;
    public GameObject suitcase;
    public Vector3 newSuitcasePos;
    public   GameObject answerGrid;
    public GameObject eventText;

    public GameObject wrongPrefab;
    public GameObject rightPrefab;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        chosenRandomEvent = allRandomEvents[Random.Range(0, allRandomEvents.Length)];

        testingIndexes = Select3RandomIndexes();
        chosenEvents = new Event[] { allRegularEvents[testingIndexes[0]], allRegularEvents[testingIndexes[1]], allRegularEvents[testingIndexes[2]], chosenRandomEvent };
        currentEvent = chosenEvents[eventCounter];
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
            drawer.SetActive(false);
            readyButton.SetActive(false);
            answerGrid.SetActive(true);
            confirmButton.SetActive(true);
            suitcase.transform.position = newSuitcasePos;

            dialogueManager.EnterEventMode();   
        }

    }

    public void PressConfirm()
    {
        bool eventPassed = false;
        if ( answerObject!=null)
        {

            for (int i = 0; i < currentEvent.correctObjects.Length; i++) 
            {
                if (answerObject.gameObject.tag == currentEvent.correctObjects[i])
                {
                    
                    Debug.Log("Win");
                    confirmButton.SetActive(false); // hay q acordarse de settearlo active de nuevo para el siguiente evento, en el sitio correcto (dialogue manager?)
                    eventPassed = true;
                    dialogueManager.DisplayWinDialogue(eventCounter, i);
                    eventCounter++;
                    if(eventCounter<4)
                    { currentEvent = chosenEvents[eventCounter]; }
                    break;
                }
            } 
            if (eventPassed == false)
            {
                Debug.Log("Lose");
                confirmButton.SetActive(false);
                dialogueManager.DisplayLoseDialogue(eventCounter);
                Instantiate(wrongPrefab, eventsDisplay[eventCounter].transform.position, Quaternion.identity);
            }
        }
    }

    public void PrepareNextEvent()
    {
        confirmButton.SetActive(true);
        answerGridIsEmpty = true;
        //hay q hacer q todos los tiles de answergrid se pongan a empty
        foreach (Transform child in answerGrid.transform) //matar hijos de answerGrid;
        {
            if(child.gameObject.tag != "AnswerGrid")
            {
                Destroy(child.gameObject);
            }
            else 
            {
                TileBehaviour tileBehaviour = child.GetComponent<TileBehaviour>();
                if (tileBehaviour != null)
                { tileBehaviour.tileIsFree = true; }
            }
        }
    }
}
