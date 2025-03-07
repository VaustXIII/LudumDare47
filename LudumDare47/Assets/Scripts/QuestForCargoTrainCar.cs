﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestForCargoTrainCar : MonoBehaviour
{
    [SerializeField] private ChoiceCanvasController choiceCanvasController;

    bool isNextStarted;

    // Start is called before the first frame update
    void Start()
    {
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
        PlayerControl.Instance.EquipClothes((ClothesType)choice);

        if (isNextStarted) { return; }

        if ((ClothesType)choice == ClothesType.Barman)
        {
            QuestsManager.Instance.InvokeQuestCompleted(TrainCarType.Restaraunt);
        }
        else
        {
            QuestsManager.Instance.InvokeQuestUnCompleted(TrainCarType.Restaraunt);
        }
    }
}
