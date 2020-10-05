using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLast : MonoBehaviour
{
    [SerializeField] private Interactible machinistDialogueInteractible;

    [SerializeField] private DialogueLines machinistLines;

    private int linesRead = 0;

    private void Start()
    {
        machinistDialogueInteractible.PlayerInteracted += OnPlayerTalkedToMachinist;
    }

    private void OnPlayerTalkedToMachinist()
    {
        linesRead++;
        if (linesRead > machinistLines.Lines.Length) {
            SceneManager.LoadScene(1);
        }
    }
}
