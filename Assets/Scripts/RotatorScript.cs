using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public float spinSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + (spinSpeed * Time.deltaTime), 0);
    }
}
