using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    
    public Dialogue scene0StartingDialogue;
    public Dialogue scene1StartingDialogue;
    


    private Queue<string> sentences;

    private int dialogueCounter = 0;

    private bool canSkip = true;

    public int scene3EventCount=0;

    public GameObject scene1dialogue1;
    public GameObject scene1dialogue2;
    public GameObject scene1dialogue3;
    public GameObject scene1dialogue4;

    public GameObject scene3GameplayDialogue;
    public GameObject scene3EventDialogue1;
    public GameObject scene3EventDialogue2;

    public bool scene3EventMode = false;

    private bool winDialogueDone = false;
    private bool loseDialogueDone = false;

    public GameHandler gameHandler;

    void Start()
    {

        sentences = new Queue<string>();
        if(SceneManager.GetActiveScene().buildIndex==0)
        {
            StartDialogue(scene0StartingDialogue);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartDialogue(scene1StartingDialogue);
        }

    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (canSkip)
        { 
        
            if (sentences.Count==0)
            {
            EndDialogue();
            return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        int charCount = 0;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            canSkip = false;
            dialogueText.text += letter;
            charCount++;
            yield return new WaitForSeconds(0.01f);
            if (charCount == sentence.Length)
            {
                canSkip = true;
            }
        }
    }
    void EndDialogue()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            dialogueCounter++;
            if (dialogueCounter==1)
            {
                //activar la segunda caja de textos y su mensaje
                scene1dialogue2.SetActive(true);
                dialogueText = scene1dialogue2.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                nameText = scene1dialogue2.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                StartDialogue(scene1dialogue2.GetComponent<DialogueTrigger>().dialogue);
            }
            else if (dialogueCounter == 2)
            {
                //activar la tercera caja de textos y su mensaje
                scene1dialogue3.SetActive(true);
                dialogueText = scene1dialogue3.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                nameText = scene1dialogue3.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                StartDialogue(scene1dialogue3.GetComponent<DialogueTrigger>().dialogue);

            }
            else if (dialogueCounter == 3)
            {
                //activar la cuarta caja de textos y su mensaje
                scene1dialogue4.SetActive(true);
                dialogueText = scene1dialogue4.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                nameText = scene1dialogue4.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                StartDialogue(scene1dialogue4.GetComponent<DialogueTrigger>().dialogue);
            }
            else if (dialogueCounter == 4)
            {
                //cambiar de escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if(winDialogueDone == true)
            { 
                winDialogueDone = false;
                if(GameHandler.eventCounter<=3) //atento checkear este 3. puede estar mal
                    LoadNextEvent();
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }else if(loseDialogueDone==true)
            {
                Debug.Log("YOU LOSE");  // aqui hay q hacer q se diplayee el texto de perder
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

        }
    }

    public void EnterEventMode()
    {
        scene3EventMode = true;
        scene3EventDialogue1.SetActive(true);
        dialogueText = scene3EventDialogue1.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        nameText = scene3EventDialogue1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        StartDialogue(GameHandler.chosenEvents[0].initialDialogue);
    }

    public void LoadNextEvent()
    {
        gameHandler.PrepareNextEvent();
        scene3EventDialogue2.SetActive(false);
        scene3EventDialogue1.SetActive(true);
        dialogueText = scene3EventDialogue1.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        nameText = scene3EventDialogue1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        StartDialogue(GameHandler.chosenEvents[GameHandler.eventCounter].initialDialogue);
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSkip)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 && scene3EventMode==true)
            {
                scene3EventDialogue2.SetActive(true);
                dialogueText = scene3EventDialogue2.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
                scene3EventDialogue2.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = scene3EventDialogue1.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;
            }
            DisplayNextSentence();
        }
    }

    public void DisplayWinDialogue(int eventCount, int objectIndex)
    {
        scene3EventDialogue2.SetActive(false);
        scene3EventDialogue1.SetActive(true);
        dialogueText = scene3EventDialogue1.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        nameText = scene3EventDialogue1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        StartDialogue(GameHandler.chosenEvents[eventCount].correctDialogues[objectIndex]);
        winDialogueDone = true;
    }

    public void DisplayLoseDialogue(int eventCount)
    {
        scene3EventDialogue2.SetActive(false);
        scene3EventDialogue1.SetActive(true);
        dialogueText = scene3EventDialogue1.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        nameText = scene3EventDialogue1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        StartDialogue(GameHandler.chosenEvents[eventCount].loseDialogue);
        loseDialogueDone = true;

    }

}
