using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TrainCarLooper : MonoBehaviour
{
    [SerializeField] private List<TrainCar> trainCarPrefabs;

    private List<TrainCar> trainCars;
    private TrainCar leftTrainCar;
    private TrainCar rightTrainCar;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeTrainCars();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeTrainCars()
    {
        int trainCarsCount = trainCarPrefabs.Count;
        Assert.IsTrue(trainCarsCount > 0, "Must have at least one train car");

        trainCars = new List<TrainCar>(trainCarsCount + 2); // All prefabs + 2 on left and right for looping

        TrainCar previous = null;
        foreach (var trainCarPrefab in trainCarPrefabs)
        {
            var trainCar = Instantiate<TrainCar>(trainCarPrefab, transform);
            if (previous != null)
            {
                ConnectTrainCars(previous, trainCar);
            }
            trainCars.Add(trainCar);
            previous = trainCar;
        }

        var left = trainCarPrefabs[trainCarsCount - 1];
        var right = trainCarPrefabs[0];
        leftTrainCar = Instantiate<TrainCar>(left, transform);
        rightTrainCar = Instantiate<TrainCar>(right, transform);

        ConnectTrainCars(trainCars[trainCarsCount - 1], rightTrainCar);
        trainCars.Add(rightTrainCar);

        ConnectTrainCars(leftTrainCar, trainCars[0], true);
        trainCars.Insert(0, leftTrainCar);
    }

    private void ConnectTrainCars(TrainCar left, TrainCar right, bool isMovingLeft = false)
    {
        float distanceBetweenCurrentAndPrevious = left.RightAnchor.localPosition.x
                    + (-right.LeftAnchor.localPosition.x);
        if (isMovingLeft)
        {
            left.transform.Translate(
                right.transform.localPosition.x - distanceBetweenCurrentAndPrevious, 0f, 0f);
        }
        else
        {
            right.transform.Translate(
                left.transform.localPosition.x + distanceBetweenCurrentAndPrevious, 0f, 0f);

        }

    }
}
