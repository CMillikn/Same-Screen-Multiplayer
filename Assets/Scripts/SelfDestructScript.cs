using System.Collections;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    public float selfDestructTime;
    void Start()
    {
        StartCoroutine(Self());
    }

    IEnumerator Self()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}
