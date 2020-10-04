using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum TrainCarType
{
    Plain = 0,
    Passenger,
    Cargo,

}

public class TrainCarLooper : MonoBehaviour
{
    [SerializeField] private List<TrainCar> trainCarPrefabs;

    private List<TrainCar> trainCars;
    private TrainCar leftTrainCar;
    private TrainCar rightTrainCar;

    // Start is called before the first frame update
    private void Awake()
    {
        InitializeTrainCars();
    }

    private void Start() {
        QuestsManager.Instance.QuestCompleted += AddTrainCarToTrain;
    }

    public void AddTrainCarToTrain(int carType) {
        AddTrainCarToTrain((TrainCarType) carType);
    }

    public void AddTrainCarToTrain(TrainCarType carType) {
        int indexOfLast = trainCars.Count - 1;
        var oldCar = trainCars[indexOfLast];
        trainCars.Remove(oldCar);
        oldCar.transform.Translate(-oldCar.transform.localPosition.x, 0f, 0f);
        var newCar = Instantiate<TrainCar>(trainCarPrefabs[(int)carType], transform);
        var plainCar = Instantiate<TrainCar>(trainCarPrefabs[(int)TrainCarType.Plain], transform);

        trainCars.Add(newCar);
        trainCars.Add(plainCar);
        trainCars.Add(oldCar);
        ConnectTrainCars(trainCars[indexOfLast-1], newCar);
        ConnectTrainCars(newCar, plainCar);
        ConnectTrainCars(plainCar, oldCar);
    }

    private void InitializeTrainCars()
    {
        trainCars = new List<TrainCar>(4);

        TrainCarType[] toInstatiate = { TrainCarType.Plain, TrainCarType.Passenger, TrainCarType.Plain, TrainCarType.Passenger };

        TrainCar previous = null;
        foreach (var carType in toInstatiate)
        {
            var trainCar = Instantiate<TrainCar>(trainCarPrefabs[(int)carType], transform);
            if (previous != null)
            {
                ConnectTrainCars(previous, trainCar);
            }
            trainCars.Add(trainCar);
            previous = trainCar;
        }

        transform.Translate(
            trainCars[0].transform.localPosition.x - trainCars[1].transform.localPosition.x, 0f, 0f);

        var leftTrainCar = trainCars[0];
        var rightTrainCar = trainCars[trainCars.Count - 1];
        leftTrainCar.PlayerEntered += OnPlayerReachedLeft;
        rightTrainCar.PlayerEntered += OnPlayerReachedRight;
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

    private void OnPlayerReachedLeft()
    {
        var distanceToMove = trainCars[trainCars.Count - 2].transform.localPosition.x
            - trainCars[0].transform.localPosition.x;

        transform.Translate(-distanceToMove, 0f, 0f);
        QuestsManager.Instance.InvokePlayerWarped();
        
    }

    private void OnPlayerReachedRight()
    {
        var distanceToMove = trainCars[trainCars.Count - 1].transform.localPosition.x
            - trainCars[1].transform.localPosition.x;

        transform.Translate(distanceToMove, 0f, 0f);
        QuestsManager.Instance.InvokePlayerWarped();

    }
}
