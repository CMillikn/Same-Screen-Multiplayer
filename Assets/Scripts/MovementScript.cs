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
    [SerializeField] GameObject slapObject;
    private bool isSlapped;
    private bool isP2;
    public int playerNumber;
    public Gamepad myController;

    void Start()
    {
        _thisRB = this.GetComponent<Rigidbody>();
        _thisKB = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSlapped)
        {
            if (_thisRB.linearVelocity.magnitude > playerSpeed)
            {
                _thisRB.linearVelocity = Vector3.ClampMagnitude(_thisRB.linearVelocity, playerSpeed);
            }

            if (myController.leftStick.up.isPressed)
            {
                _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z + playerAccel * Time.deltaTime));
            }
        
            if (myController.leftStick.down.isPressed)
            {
                _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z - playerAccel * Time.deltaTime));
            }

            if (myController.leftStick.left.isPressed)
            {
                _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x - playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
            }

            if (myController.leftStick.right.isPressed)
            {
                _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x + playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
            }

            if (_thisKB.rightShiftKey.isPressed)
            {
                GameObject slapInstant = Instantiate(slapObject, this.transform.position, this.transform.rotation);
                slapInstant.transform.parent = this.transform;
            }
        }
    }

    public void GetSlapped()
    {
        StartCoroutine(Slapped());
    }

    IEnumerator Slapped()
    {
        isSlapped = true;
        yield return new WaitForSeconds(1);
        isSlapped = false;
    }
}
