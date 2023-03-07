using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = 10f; // T?c ?? di chuy?n c?a ??i t??ng

    void Update()
    {
        // Di chuy?n ??i t??ng sang trái/ph?i n?u nh?n phím m?i tên trái/ph?i
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
    }
}
