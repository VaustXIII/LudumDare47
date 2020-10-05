using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestForCargoTrainCar : MonoBehaviour
{
    [SerializeField] private ChoiceCanvasController choiceCanvasController;
    private bool[] choicesMade = new bool[3]; // РАЗМЕР - КОЛИЧЕСТВО ВЫБОРОВ В ПРЕВОМ ВАГОНЕ!!!
    private bool hasWarpedAfterChoice = true;

    // Start is called before the first frame update
    void Start()
    {
        QuestsManager.Instance.PlayerWarpedAround += OnPlayerWarped;
        choiceCanvasController.PlayerHasChosen += OnPlayerHasChosen;
    }

    private void OnPlayerWarped()
    {
        hasWarpedAfterChoice = true;
    }

    private void OnPlayerHasChosen(int choice)
    {
        PlayerControl.Instance.EquipClothes((ClothesType) choice);
        
        if (!hasWarpedAfterChoice) { return; }

        choicesMade[choice] = true;
        hasWarpedAfterChoice = false;

        foreach (var isChoiceMade in choicesMade)
        {
            if (!isChoiceMade) return;
        }
        QuestsManager.Instance.InvokeQuestCompleted(TrainCarType.Cargo);
    }
}
