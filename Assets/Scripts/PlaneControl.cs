using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{

    // Delay between audio and video
    public int delayInMs = 0;

    public float isi = 1f;
    
    // audioSource
    private AudioSource audioSource;

    private float timer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // turn plane invisble
        GetComponent<Renderer>().enabled = false;

        // get audio source
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // increase mscount by delta time
        timer += Time.deltaTime;
        


        if (timer >= isi)
        {
            // print timer
            Debug.Log((timer - isi)*1000);
            // reset mscount
            timer = 0;

            // turn plane visible for 100ms
            GetComponent<Renderer>().enabled = true;
            StartCoroutine(DisablePlane(100));
            PlayAudio();
        }
    }

    IEnumerator DisablePlane(int ms)
    {
        yield return new WaitForSeconds(ms / 1000f);
        GetComponent<Renderer>().enabled = false;
    }

    void PlayAudio()
    {
        // wait ms
        audioSource.PlayOneShot(audioSource.clip);
        return;
    }

}
