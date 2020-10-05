using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCanvasController : MonoBehaviour
{
    public event Action<int> PlayerHasChosen;

    [SerializeField] private Interactible bindedInteractible;
    [SerializeField] private Canvas choiceCanvas;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        bindedInteractible.PlayerInteracted += OnInteracted;
        bindedInteractible.PlayerLeft += OnPlayerLeft;
        source = GetComponent<AudioSource>();
    }
    
    public void Choose(int choice) {
        Debug.Log($"Chose {choice}");
        PlayerHasChosen?.Invoke(choice);
        choiceCanvas.gameObject.SetActive(false);
        source.Play();
    }

    private void OnInteracted() {
        choiceCanvas.gameObject.SetActive(true);
        
    }

    private void OnPlayerLeft() {
        choiceCanvas.gameObject.SetActive(false);

    }
}
