using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestForBarTrainCar : MonoBehaviour
{
    [SerializeField] private ChoiceCanvasController choiceCanvasController;
    [SerializeField] private Interactible barDudeDialogueInteractible;

    // Start is called before the first frame update
    void Start()
    {
        barDudeDialogueInteractible.PlayerInteracted += () => QuestsManager.Instance.InvokeQuestStarted(TrainCarType.Restaraunt);
        choiceCanvasController.PlayerHasChosen += OnPlayerHasChosen;
    }

    private void OnPlayerHasChosen(int choice)
    {
        PlayerControl.Instance.EquipDrink((DrinkType)choice);

        if ((DrinkType)choice == DrinkType.Drink3) {
            QuestsManager.Instance.InvokeQuestCompleted(TrainCarType.Head);
        }

    }
}
