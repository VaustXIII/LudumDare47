using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarLooper : MonoBehaviour
{
    [SerializeField] private List<GameObject> trainCarPrefabs;

    private List<TrainCar> trainCars;
    private TrainCar leftTrainCar;
    private TrainCar rightTrainCar;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTrainCars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeTrainCars() {

    }
}
