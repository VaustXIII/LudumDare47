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

    private BoxCollider trainCarCollider;

    // Start is called before the first frame update
    void Start()
    {
        trainCarCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"Collided with {other.tag}");
        
        if (other.GetComponent<PlayerControl>() != null) {
            PlayerEntered?.Invoke();
        }
    }

}
