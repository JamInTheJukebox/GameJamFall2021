using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerController : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == true)
        {
            StartCoroutine(FlickeringLight());
        }
    }
    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.01f, 3.0f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.01f, 3.0f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = true;
    }
}
