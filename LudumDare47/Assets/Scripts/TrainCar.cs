using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TrainCar : MonoBehaviour
{
    public event Action PlayerEntered;

    public Transform LeftAnchor => leftAnchor;
    [SerializeField] private Transform leftAnchor;
    public Transform RightAnchor => rightAnchor;
    [SerializeField] private Transform rightAnchor;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerControl>() != null)
        {
            PlayerEntered?.Invoke();
        }
    }

}
