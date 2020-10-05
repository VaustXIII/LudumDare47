using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuitCaseType {
    Red = 0,
    Green,
    Bluee
}

public enum ClothesType {
    Tuxedo = 0,
    Casual,
    Barman
}

public enum DrinkType {
    Drink1 = 0,
    Drink2,
    Drink3
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance {get; private set;}
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] GameObject[] suitCases;
    [SerializeField] GameObject[] clothes;
    [SerializeField] GameObject[] drinks;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SetVelocity();
    }

    public void EquipSuitCase(SuitCaseType suitCase) {
        foreach (var item in suitCases)
        {
            item.SetActive(false);
        }
        suitCases[(int)suitCase].SetActive(true);
    }

    public void EquipClothes(ClothesType clothesType) {
        foreach (var item in clothes)
        {
            item.SetActive(false);
        }
        clothes[(int)clothesType].SetActive(true);
    }

    public void EquipDrink(DrinkType drink) {
        foreach (var item in drinks)
        {
            item.SetActive(false);
        }
        drinks[(int)drink].SetActive(true);
    }

    private void SetVelocity() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var velocity = new Vector3(horizontal, 0f, vertical).normalized * maxSpeed;
        rb.velocity = velocity;

        if (horizontal == 0f && vertical == 0f) {
            return;
        }
        transform.LookAt(-10*velocity);
    }
}
