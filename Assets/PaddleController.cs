using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0.0f, 0.0f);
    }

    void OnMoveHorizontally(InputValue i)
    {
        speed = i.Get<float>() * 0.1f;

        
    }
}
