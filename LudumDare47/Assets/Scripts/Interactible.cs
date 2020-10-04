using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactible : MonoBehaviour
{
    [SerializeField] private GameObject interactionAvailableHint;
    [SerializeField] private SphereCollider interactionTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
