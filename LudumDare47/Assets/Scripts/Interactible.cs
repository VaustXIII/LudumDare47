using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactible : MonoBehaviour
{
    public event Action PlayerInteracted;
    public event Action PlayerLeft;

    [SerializeField] private GameObject interactionAvailableHint;
    [SerializeField] private SphereCollider interactionTrigger;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && interactionAvailableHint.activeSelf) {
            PlayerInteracted?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{gameObject.name} trigger entered by {other.name}");
        
        if (other.GetComponent<PlayerControl>() != null) {
            interactionAvailableHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log($"{gameObject.name} trigger exited by {other.name}");
        
        if (other.GetComponent<PlayerControl>() != null) {
            interactionAvailableHint.SetActive(false);
            PlayerLeft?.Invoke();
        }
    }
}
