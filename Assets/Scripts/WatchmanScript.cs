using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WatchmanScript : MonoBehaviour
{
    public GameObject centerPoint;
    public bool isKilling;
    PlayerScript playerScript;
    public float chargeTime;
    public float dangerTime;
    public float killingTime;
    public Light spotLight;
    public bool canSwap;
    public bool canSwapClock;
    public GameObject owieSplosion;
    public List<GameObject> cornerObjects;
    public float moveSpeed;
    public bool isClockwise;
    public float clockSwapTimer;
    public float clockSwapVariance;
    public float damagePlayer;
    public float speedUp;

    public enum cardinalLocation
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    cardinalLocation currentLoc;

    // Update is called once per frame
    void Update()
    {
        moveSpeed = moveSpeed + (speedUp*Time.deltaTime);
        this.transform.LookAt(centerPoint.transform.position);
        RaycastHit hit;

        //When clockwise, spins around set points
        if (isClockwise)
        {
            if (currentLoc == cardinalLocation.Top)
            {
                if (transform.position != cornerObjects[1].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[1].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Right;
                }
            }
            else if (currentLoc == cardinalLocation.Right)
            {
                if (transform.position != cornerObjects[3].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[3].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Bottom;
                }
            }
            else if (currentLoc == cardinalLocation.Bottom)
            {
                if (transform.position != cornerObjects[2].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[2].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Left;
                }
            }
            else
            {
                if (transform.position != cornerObjects[0].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[0].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Top;
                }
            }
        }
        //When counterclockwise, follows the opposite track
        else
        {
            if (currentLoc == cardinalLocation.Top)
            {
                if (transform.position != cornerObjects[0].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[0].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Left;
                }
            }
            else if (currentLoc == cardinalLocation.Left)
            {
                if (transform.position != cornerObjects[2].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[2].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Bottom;
                }
            }
            else if (currentLoc == cardinalLocation.Bottom)
            {
                if (transform.position != cornerObjects[3].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[3].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Right;
                }
            }
            else
            {
                if (transform.position != cornerObjects[1].transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cornerObjects[1].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    currentLoc = cardinalLocation.Top;
                }
            }
        }
        foreach (GameObject player in ConradGameManager.Instance.playerObjects)
        {
            if (player != null)
            {
                Vector3 pointAt = player.transform.position - this.transform.position;
                Debug.DrawRay(transform.position, pointAt * 10, Color.yellow);
                if (Physics.Raycast(transform.position, pointAt, out hit, 20))
                {
                    Debug.Log($"I'm hitting {hit.transform.name}!");
                    playerScript = hit.transform.GetComponent<PlayerScript>();
                    if (playerScript != null)
                    {
                        if (isKilling )
                        {
                            playerScript.GetHurt(damagePlayer);
                            Instantiate(owieSplosion, hit.transform.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        if (canSwap)
        {
            StartCoroutine(ChangeStates());
        }
        if (canSwapClock)
        {
            StartCoroutine(ChangeClock());
        }
    }

    IEnumerator ChangeStates()
    {
        canSwap = false;
        if (isKilling)
        {
            yield return new WaitForSeconds(killingTime);
           spotLight.color = Color.white;
            isKilling = !isKilling;
        }
        else
        {
            yield return new WaitForSeconds(chargeTime);
            spotLight.color = Color.yellow;
            yield return new WaitForSeconds(dangerTime);
            spotLight.color = Color.red;
            isKilling = !isKilling;
        }
        canSwap = true;
    }

    IEnumerator ChangeClock()
    {
        canSwapClock = false;
        isClockwise = !isClockwise;
        yield return new WaitForSeconds(Random.Range(clockSwapTimer-clockSwapVariance, clockSwapTimer+clockSwapVariance));
        canSwapClock = true;
    }
}
