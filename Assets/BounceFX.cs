using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float t = 0;

    float timeScale = 20f;
    float distScale = 0.05f;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * timeScale;

        this.transform.position = new Vector3(0,  Mathf.Sin(t) * distScale, 0);
    }
}
