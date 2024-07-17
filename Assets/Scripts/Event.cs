using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public string eventName;
    public Sprite eventArt;

    public Dialogue initialDialogue;

    public Dialogue loseDialogue;

    public string[] correctObjects;

    public Dialogue[] correctDialogues;

    
}
