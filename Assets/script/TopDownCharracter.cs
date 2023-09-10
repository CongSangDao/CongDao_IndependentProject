using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharracter : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        transform.Translate(movement * speed * Time.deltaTime);
    }
}
