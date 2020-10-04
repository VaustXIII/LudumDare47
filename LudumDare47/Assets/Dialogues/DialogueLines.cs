using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueLines", menuName = "Dialogue Lines", order = 1)]
public class DialogueLines : ScriptableObject
{
    public string[] Lines => lines;
    [SerializeField] private string[] lines;
}
