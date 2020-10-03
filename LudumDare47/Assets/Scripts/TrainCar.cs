using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour
{
    public Transform LeftAnchor => leftAnchor;
    [SerializeField] private Transform leftAnchor;
    public Transform RightAnchor => rightAnchor;
    [SerializeField] private Transform rightAnchor;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
