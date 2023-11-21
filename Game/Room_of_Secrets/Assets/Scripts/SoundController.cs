using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    //TODO : Add the footsteps sounds every time the player is moving
    private AudioSource footstepsAudio;
    //TODO : Player the music only when there is an input
    void Start()
    {
        footstepsAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInputPressed())
        {
            if(!footstepsAudio.isPlaying) {
                Debug.Log("The music should play now");
                footstepsAudio.Play();
            }
            
        }
        else {
            Debug.Log("The music should not play now");
            footstepsAudio.Pause();
        }
    }

    /// <summary>
    /// check if an input is recieved
    /// </summary>
    /// <returns>true if Input recieved , false otherwise</returns>
    public bool hasInputPressed() {

        return (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
    }
}
