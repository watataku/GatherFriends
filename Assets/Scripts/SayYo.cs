using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayYo : MonoBehaviour
{
    public int score = 1;
    private GameObject voice;
    // Start is called before the first frame update
    void Start()
    {
        voice = transform.GetChild(0).gameObject;
        voice.SetActive(false);
    }

    public void Yo()
    {
        StartCoroutine("Calling");
    }

    private IEnumerator Calling()
    {
        score = 0;
        voice.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
