using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public static QuestsManager Instance { get; private set; }

    public event Action<TrainCarType> QuestComplete;
    public event Action PlayerWarpedAround;

    // Start is called before the first frame update
    void Awake()
    {   
        if (Instance != null) {
            Debug.LogError($"Duplicate {name}");
        }
        Instance = this;
    }
}
