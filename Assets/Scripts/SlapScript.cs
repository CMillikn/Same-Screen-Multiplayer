using Unity.VisualScripting;
using UnityEngine;

public class SlapScript : MonoBehaviour
{
    public int originPlayerNumber;
    public GameObject slapPuff;
    public void Awake()
    {
        Instantiate(slapPuff,transform.position,Quaternion.identity);
    }
}
