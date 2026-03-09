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
            if (isP2 != true)
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

                if (_thisKB.rightShiftKey.isPressed)
                {
                    
                }
            }
            else
            {
                if (_thisRB.linearVelocity.magnitude > playerSpeed)
                {
                    _thisRB.linearVelocity = Vector3.ClampMagnitude(_thisRB.linearVelocity, playerSpeed);
                }

                if (_thisKB.iKey.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z + playerAccel * Time.deltaTime));
                }

                if (_thisKB.kKey.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z - playerAccel * Time.deltaTime));
                }

                if (_thisKB.jKey.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x - playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
                }

                if (_thisKB.lKey.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x + playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
                }

                if (_thisKB.leftShiftKey.isPressed)
                {

                }
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
