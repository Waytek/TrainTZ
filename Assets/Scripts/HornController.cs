using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornController : MonoBehaviour {
    AudioSource horn;
	// Use this for initialization
    void Start()
    {
        horn = GetComponent<AudioSource>();
    }
    public void PlayHorn()
    {
        StopAllCoroutines();
        horn.volume = 1;
        horn.Play();
    }
    public void StopHorn()
    {
        //horn.Stop();
        StartCoroutine(Fading());
    }
    IEnumerator Fading()
    {

        while(horn.volume > 0)
        {
            horn.volume -= 0.05f;
            if (horn.volume < 0.05f)
            {
                horn.Stop();
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
