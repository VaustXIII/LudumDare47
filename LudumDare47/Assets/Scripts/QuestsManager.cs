using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public static QuestsManager Instance { get; private set; }

    public event Action<TrainCarType> QuestStarted;
    public event Action<TrainCarType> QuestCompleted;
    public event Action<TrainCarType> QuestUnCompleted;
    public event Action PlayerWarpedAround;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Duplicate {name}");
        }
        Instance = this;
    }

    public void DebugQuestCompleted(int quest)
    {
        switch (quest)
        {
            case 0:
                QuestsManager.Instance.InvokeQuestCompleted(TrainCarType.Cargo);
                break;
            case 1:
                QuestsManager.Instance.InvokeQuestCompleted(TrainCarType.Restaraunt);
                break;
            default:
                break;
        }
    }

    public void InvokeQuestStarted(TrainCarType unlockedTrainCar)
    {
        QuestStarted?.Invoke(unlockedTrainCar);
        Debug.Log($"Quest started, {unlockedTrainCar}");
    }

    public void InvokeQuestCompleted(TrainCarType unlockedTrainCar)
    {
        QuestCompleted?.Invoke(unlockedTrainCar);
        Debug.Log($"Quest completed, {unlockedTrainCar} unlocked");
    }

    public void InvokeQuestUnCompleted(TrainCarType unlockedTrainCar)
    {
        QuestUnCompleted?.Invoke(unlockedTrainCar);
        Debug.Log($"Quest uncompleted, {unlockedTrainCar} locked");
    }

    public void InvokePlayerWarped()
    {
        PlayerWarpedAround?.Invoke();
        Debug.Log($"Player warped");
    }
}
