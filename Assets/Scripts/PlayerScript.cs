using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> watcherObjects = new List<GameObject>();
    [SerializeField] List<Light> ominousLights = new List<Light>();
    [SerializeField] GameObject _thisRB;
    MovementScript msScript;
    private bool isSearching;
    private bool isKILLING;
    private int randomListObject;
    [SerializeField] public float waitTime;
    [SerializeField] private Light evilLight;

    // Update is called once per frame
    void Update()
    {
        watcherObjects[0].transform.position = new Vector3(_thisRB.transform.position.x, 0.5f, 4.5f);
        watcherObjects[1].transform.position = new Vector3(_thisRB.transform.position.x, 0.5f, -4.5f);
        watcherObjects[2].transform.position = new Vector3(-4.5f, 0.5f, _thisRB.transform.position.z);
        watcherObjects[3].transform.position = new Vector3(4.5f, 0.5f, _thisRB.transform.position.z);
        if (!isSearching)
        {
            StartCoroutine(startSearch());
        }

        if (isKILLING)
        {
            KillRay(randomListObject);
        }
    }

    IEnumerator startSearch()
    {
        isSearching = true;
        
        Vector3 startSize = watcherObjects[0].transform.localScale;
        randomListObject =  Random.Range(0, watcherObjects.Count);
        Light currentLight = ominousLights[randomListObject];
        currentLight.intensity = 1;
        currentLight.color = Color.yellow;
        watcherObjects[randomListObject].transform.localScale =
            new Vector3(startSize.x + 1, startSize.y + 1, startSize.z + 1);
        yield return new WaitForSeconds(waitTime);
        currentLight.color = Color.red;
        isKILLING = true;
        yield return new WaitForSeconds(waitTime);
        currentLight.intensity = 0;
        watcherObjects[randomListObject].transform.localScale = startSize;
        isKILLING = false;
        yield return new WaitForSeconds(waitTime/2);
        isSearching = false;
    }

    void KillRay(int randomCount)
    {
        Debug.DrawRay(watcherObjects[randomListObject].transform.position, watcherObjects[randomListObject].transform.forward);
        LayerMask layerMask = LayerMask.GetMask("Walls","Player");
        if (Physics.Raycast(watcherObjects[randomCount].transform.position, 
                transform.TransformDirection(watcherObjects[randomCount].transform.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            msScript = hit.collider.gameObject.GetComponent<MovementScript>();
            if (msScript != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
