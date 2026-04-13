CS 480 Project 2
Team: James Smith, Jack Lund, Lucian Murphy

/// DOT PRODUCT -- Jack Lund ///


/// LINEAR INTERPOLATION -- Lucian Murphy ///


/// PARTICLES W TRIGGER -- James Smith ///
When I started doing this, I knew I wanted to add some sort of blood particle. In class I know how to make the particle but I had no idea how to make it activate via a trigger. I found a YouTube video that showed me how to make a "triggered" particle system. In the video the instructor make a sort of explosion. I used the guidance from the video to help me understand to create what I wanted. I started by making the system a burst rather than a constant flow of particles. I did this by turning off looping, and making the duration 0.05 seconds. Then I created a dark red, and smooth material for the particles. I also made the particles round by making the renderer a sphereical mesh. 

The idea for the trigger was simple. Get caught = die = trigger blood. To do this I found where the getCaught mechanic was, which was in the GameEnding script, and added the code for activating the particle.

public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
        //Trigger for the particles
        caughtEffect.Play();
    }

/// SOUND W TRIGGER -- James Smith ///
For the sound trigger I once again went to YouTube for answers. I found the easiest way to do a sound trigger was to make an empty game object with a 3D collider. So I knew how to do the sound trigger, but I didn't know what to make it. I noticed that there was a ghost in the game just in the bathroom taking a shower. So, I thought it would be funny to add a scream effect when you walked into the bathroom. To do this, I downloaded a female scream WAV from the internet (royalty free, fiscally free) and added a sound emittor component to the empty object. Then I added a script (AudioTriggerScript) that plays the sound when you walk into the collider.

public class AudioTriggerScript : MonoBehaviour
{
    AudioSource audioSource;
    Collider collider;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collider) {
        audioSource.Play();
    }
}
