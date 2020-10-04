using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class DialogueController : MonoBehaviour
{
    public DialogueLines dialogue;

    private int? currentLine;

    [SerializeField] private Interactible interactible;
    [SerializeField] private Canvas textCanvas;
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(interactible, "DialogueController needs linked interactible");
        Assert.IsNotNull(dialogue, "DialogueController needs dialobue lines");
        Assert.IsTrue(dialogue.Lines.Length > 0, "DialogueController needs at least one line of dialogue");

        interactible.PlayerInteracted += OnPlayerInteracted;
        interactible.PlayerLeft += OnPlayerLeft;
    }

    private void OnPlayerInteracted()
    {
        if (currentLine == null)
        {
            currentLine = 0;
            textCanvas.gameObject.SetActive(true);
            text.text = dialogue.Lines[0];
        }
        else
        {
            currentLine++;
            if (currentLine >= dialogue.Lines.Length) {
                currentLine = null;
                textCanvas.gameObject.SetActive(false);
            } else {
                textCanvas.gameObject.SetActive(true);
                text.text = dialogue.Lines[currentLine.Value];
            }
        }
    }

    private void OnPlayerLeft() {
        textCanvas.gameObject.SetActive(false);
    }
}
