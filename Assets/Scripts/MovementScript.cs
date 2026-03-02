using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

public class MovementScript : MonoBehaviour
{
    Keyboard _thisKB;
    Rigidbody _thisRB;
    [SerializeField] List<GameObject> watcherObjects = new List<GameObject>();
    [SerializeField] float playerSpeed;
    [SerializeField] float playerAccel;

    void Start()
    {
        _thisRB = this.GetComponent<Rigidbody>();
        _thisKB = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (_thisRB.linearVelocity.magnitude > playerSpeed)
        {
            _thisRB.linearVelocity = Vector3.ClampMagnitude(_thisRB.linearVelocity, playerSpeed);
        }

        if (_thisKB.wKey.isPressed)
        {
            _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z + playerAccel * Time.deltaTime));
        }
        
        if (_thisKB.sKey.isPressed)
        {
            _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z - playerAccel * Time.deltaTime));
        }

        if (_thisKB.aKey.isPressed)
        {
            _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x - playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
        }

        if (_thisKB.dKey.isPressed)
        {
            _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x + playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
        }

        watcherObjects[0].transform.position = new Vector3(this.transform.position.x, 0.5f, 4.5f);
        watcherObjects[1].transform.position = new Vector3(this.transform.position.x, 0.5f, -4.5f);
        watcherObjects[2].transform.position = new Vector3(-4.5f, 0.5f, this.transform.position.z);
        watcherObjects[3].transform.position = new Vector3(4.5f, 0.5f, this.transform.position.z);
    }
}
