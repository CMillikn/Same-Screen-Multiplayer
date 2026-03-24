using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    Keyboard _thisKB;
    Rigidbody _thisRB;
    private Material _thisMat;
    [SerializeField] List<GameObject> watcherObjects = new List<GameObject>();
    [SerializeField] float playerSpeed;
    [SerializeField] float playerAccel;
    [SerializeField] GameObject slapObject;
    private bool isSlapped;
    private GameObject slapper;
    public float slapKnockback;
    public float slapCooldown;
    private bool canSlap = true;
    private SlapScript _slapScript;
    private SlapScript _enemySlapScript;
    private bool isP2;
    public int playerNumber;
    public Gamepad myController;
    public Joystick myJoystick;
    public bool isGamepadControlled;
    public float magnitudeDisplay;
    [SerializeField] private ParticleSystem smokeCloud;
    public Image healthBarFill;

    void Start()
    {
        //if (myController == null)
        //{
            //myController = Gamepad.current;
        //}
        _thisRB = this.GetComponent<Rigidbody>();
        _thisKB = Keyboard.current;
        _thisMat = this.GetComponent<MeshRenderer>().material;
        if (playerNumber == 1)
        {
            _thisMat.color = Color.red;
            healthBarFill.color = Color.red;
        }
        else if (playerNumber == 2)
        {
            _thisMat.color = Color.yellow;
            healthBarFill.color = Color.yellow;
        }
        else if (playerNumber == 3)
        {
            _thisMat.color = Color.green;
            healthBarFill.color = Color.green;
        }
        else
        {
            _thisMat.color = Color.cyan;
            healthBarFill.color = Color.cyan;
        }
    }

    // Update is called once per frame
    void Update()
    {
        magnitudeDisplay = _thisRB.linearVelocity.magnitude;
        if (_thisRB.linearVelocity.magnitude > 0.3f)
        {
            smokeCloud.Play();
        }
        else
        {
            smokeCloud.Stop();
        }

        
        if (!isSlapped)
        {
            if (isGamepadControlled)
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
    
                if (myController.buttonEast.wasPressedThisFrame || myController.buttonNorth.wasPressedThisFrame || myController.buttonSouth.wasPressedThisFrame || myController.buttonWest.wasPressedThisFrame)
                {
                    if (canSlap)
                    {
                        StartCoroutine(SlapRecharge());
                        GameObject slapInstant = Instantiate(slapObject, this.transform.position, this.transform.rotation);
                        slapInstant.transform.parent = this.transform;
                        _slapScript = slapInstant.GetComponent<SlapScript>();
                        _slapScript.originPlayerNumber = playerNumber;
                    }
                }
            }
            else
            {
                if (_thisRB.linearVelocity.magnitude > playerSpeed)
                {
                    _thisRB.linearVelocity = Vector3.ClampMagnitude(_thisRB.linearVelocity, playerSpeed);
                }
    
                if (myJoystick.stick.up.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z + playerAccel * Time.deltaTime));
                }
            
                if (myJoystick.stick.down.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3(_thisRB.linearVelocity.x, 0, (_thisRB.linearVelocity.z - playerAccel * Time.deltaTime));
                }
    
                if (myJoystick.stick.left.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x - playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
                }
    
                if (myJoystick.stick.right.isPressed)
                {
                    _thisRB.linearVelocity = new Vector3((_thisRB.linearVelocity.x + playerAccel * Time.deltaTime), 0, _thisRB.linearVelocity.z);
                }
    
                if (myJoystick.trigger.wasPressedThisFrame)
                {
                    if (canSlap)
                    {
                        StartCoroutine(SlapRecharge());
                        GameObject slapInstant = Instantiate(slapObject, this.transform.position, this.transform.rotation);
                        slapInstant.transform.parent = this.transform;
                        _slapScript = slapInstant.GetComponent<SlapScript>();
                        _slapScript.originPlayerNumber = playerNumber;
                    }

                }
            }
        }
    }

    public void GetSlapped(Vector3 slapperPos)
    {
        StartCoroutine(Slapped());
        Vector3 slapDirection = this.transform.position - slapperPos;
        _thisRB.AddForce(slapDirection.normalized * slapKnockback, ForceMode.Impulse);
    }

    IEnumerator Slapped()
    {
        isSlapped = true;
        yield return new WaitForSeconds(1);
        isSlapped = false;
    }

    IEnumerator SlapRecharge()
    {
        canSlap = false;
        yield return new WaitForSeconds(slapCooldown);
        canSlap = true; 
    }

    private void OnTriggerEnter(Collider trig)
    {
        _enemySlapScript = trig.GetComponent<SlapScript>();
        if (_enemySlapScript != null)
        {
            if (_enemySlapScript.originPlayerNumber == playerNumber)
                return;
            GetSlapped(trig.transform.position);
        }

    }
}
