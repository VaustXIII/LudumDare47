using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestForBarTrainCar : MonoBehaviour
{
    [SerializeField] private ChoiceCanvasController choiceCanvasController;
    [SerializeField] private Interactible barDudeDialogueInteractible;

    bool isNextStarted;

    // Start is called before the first frame update
    void Start()
    {
        barDudeDialogueInteractible.PlayerInteracted += () => QuestsManager.Instance.InvokeQuestStarted(TrainCarType.Restaraunt);
        choiceCanvasController.PlayerHasChosen += OnPlayerHasChosen;
        QuestsManager.Instance.QuestStarted += OnQuestStarted;
    }

    private void OnQuestStarted(TrainCarType carType)
    {
        if (carType == TrainCarType.Restaraunt)
        {
            isNextStarted = true;
        }
    }

    private void OnPlayerHasChosen(int choice)
    {
        PlayerControl.Instance.EquipDrink((DrinkType)choice);

        if (isNextStarted) { return; }

    }
}
