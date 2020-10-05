using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] bool onlyX;

    private Vector3 fixedDistance;

    // Start is called before the first frame update
    void Start()
    {
        fixedDistance = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (onlyX) {
            transform.Translate(-transform.position.x + fixedDistance.x, -0f, 0f);
            return;
        }
        transform.position = target.transform.position + fixedDistance;
    }
}
