using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float speed_ = 10;

    Rigidbody body_;

    // Update is called once per frame
    void Update()
    {
        body_.AddForce(Vector3.forward * Time.deltaTime * speed_);
    }
}
