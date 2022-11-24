using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OVR {

    public class PlaneControl1 : MonoBehaviour
    {
        public int delayInMs = 0;
        public float isi = 1f;
        public bool isPlaying = false;

        public SoundFXRef sound;
        private float timer = 0.0f;

        private Transform planeTransform;


        // Start is called before the first frame update
        void Start()
        {
            // turn plane invisble
            //GetComponent<Renderer>().enabled = false;

            // get plane transform
            planeTransform = GetComponent<Transform>();

            // set relative position
            planeTransform.localPosition = new Vector3(0, -1000, 1);


            // get audio source
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

            if (isPlaying)
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
                DisablePlane();
            }

        }

        void OnDisable()
        {
            //GetComponent<Renderer>().enabled = false;
            StopAllCoroutines();
            isPlaying = false;
            //sound.StopSound();

        }

        void DisablePlane()
        {
            //GetComponent<Renderer>().enabled = false;
            planeTransform.localPosition = new Vector3(0, -1000, 1);
            sound.StopSound();

        }

        void EnablePlane()
        {
            //GetComponent<Renderer>().enabled = true;
            planeTransform.localPosition = new Vector3(0, 0, 1);

        }

        void PlayAudio()
        {
            // wait ms
            //audioSource.PlayOneShot(audioSource.clip);
            sound.PlaySound();
            return;
        }

        IEnumerator DisablePlaneWithDelay(int ms)
        {
            yield return new WaitForSeconds(ms / 1000f);
            planeTransform.localPosition = new Vector3(0, -1000, 1);
            //sound.StopSound();

            //GetComponent<Renderer>().enabled = false;
        }
    }
}