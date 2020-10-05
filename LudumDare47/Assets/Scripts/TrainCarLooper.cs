using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum TrainCarType
{
    Plain = 0,
    Passenger,
    Cargo,
    Restaraunt,
    Head

}

public class TrainCarLooper : MonoBehaviour
{
    [SerializeField] private List<TrainCar> trainCarPrefabs;

    private List<TrainCar> trainCars;
    private TrainCar leftTrainCar;
    private TrainCar rightTrainCar;


    private bool isBarSpawned;
    private bool isHeadSpawned;

    // Start is called before the first frame update
    private void Awake()
    {
        InitializeTrainCars();
    }

    private void Start()
    {
        QuestsManager.Instance.QuestCompleted += AddTrainCarToTrain;
        QuestsManager.Instance.QuestUnCompleted += OnQuestUncompleted;
    }

    public void AddTrainCarToTrain(int carType)
    {
        AddTrainCarToTrain((TrainCarType)carType);
    }

    public void AddTrainCarToTrain(TrainCarType carType)
    {
        if (carType == TrainCarType.Restaraunt && isBarSpawned) { return; }
        if (carType == TrainCarType.Head && isHeadSpawned) { return; }
        isBarSpawned = carType == TrainCarType.Restaraunt;
        isHeadSpawned = carType == TrainCarType.Head;

        int indexOfLast = trainCars.Count - 1;
        var oldCar = trainCars[indexOfLast];
        trainCars.Remove(oldCar);
        oldCar.transform.Translate(-oldCar.transform.localPosition.x, 0f, 0f);
        var newCar = Instantiate<TrainCar>(trainCarPrefabs[(int)carType], transform);
        var plainCar = Instantiate<TrainCar>(trainCarPrefabs[(int)TrainCarType.Plain], transform);

        trainCars.Add(newCar);
        trainCars.Add(plainCar);
        trainCars.Add(oldCar);
        ConnectTrainCars(trainCars[indexOfLast - 1], newCar);
        ConnectTrainCars(newCar, plainCar);
        ConnectTrainCars(plainCar, oldCar);

        if (carType == TrainCarType.Head) {
            var first = trainCars[0];
            var last = trainCars[trainCars.Count - 1];

            trainCars.Remove(first);
            trainCars.Remove(last);
            first.PlayerEntered -= OnPlayerReachedLeft;
            last.PlayerEntered -= OnPlayerReachedRight;
            Destroy(first.gameObject);
            Destroy(last.gameObject);
        }
    }

    private void OnQuestUncompleted(TrainCarType carType)
    {
        Debug.Log($"Handling uncomleted for {carType}, isBarSpawned: {isBarSpawned}, isHeadSpawned: {isHeadSpawned}");
        switch (carType)
        {
            case TrainCarType.Restaraunt:
                if (isBarSpawned)
                {
                    isBarSpawned = false;
                    RemoveOneTrainCar();
                }
                break;
            case TrainCarType.Head:
                if (isHeadSpawned)
                {
                    isHeadSpawned = false;
                    RemoveOneTrainCar();
                }
                break;
            default:
                Debug.Log($"Don't care about uncompleted {carType}");
                break;
        }
    }

    private void RemoveOneTrainCar()
    {
        var count = trainCars.Count;

        var last = trainCars[count - 2];
        var beforeLast = trainCars[count - 3];

        trainCars.Remove(last);
        trainCars.Remove(beforeLast);
        Destroy(last.gameObject);
        Destroy(beforeLast.gameObject);

        count = trainCars.Count;
        ConnectTrainCars(trainCars[count - 2], trainCars[count - 1]);
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
            right.transform.Translate(-right.transform.localPosition.x, 0f, 0f);
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
