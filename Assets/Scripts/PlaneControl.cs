using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public int delayInMs = 0;
    public float isi = 1f;
    public bool isPlaying = false;
    
    public UnityEngine.EventSystems.OVRInputModule InputModule;

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
        
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            isPlaying = !isPlaying;
        }

        // if thumbthis is left, decrease isi
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            this.isi -= 0.01f;
        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            this.isi += 0.01f;
        }

        if(isPlaying)
        {
            timer += Time.deltaTime;
            if (timer >= isi)
                {
                    timer = 0;

                    // turn plane visible for 500ms

                    PlayAudio();
                    EnablePlane();
                    StartCoroutine(DisablePlaneWithDelay(100));
                }
        }
        else
        {
            // turn plane invisible
            GetComponent<Renderer>().enabled = false;
        }

    }

    void OnDisable()
    {
        GetComponent<Renderer>().enabled = false;
        StopAllCoroutines();
        isPlaying = false;
    }

    void DisablePlane()
    {
        GetComponent<Renderer>().enabled = false;
    }

    void EnablePlane()
    {
        GetComponent<Renderer>().enabled = true;
    }

    void PlayAudio()
    {
        // wait ms
        audioSource.PlayOneShot(audioSource.clip);
        return;
    } 

    IEnumerator DisablePlaneWithDelay(int ms)
    {
        yield return new WaitForSeconds(ms / 1000f);
        GetComponent<Renderer>().enabled = false;
    }
}
