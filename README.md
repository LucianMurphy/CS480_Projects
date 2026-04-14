CS 480 Project 2
Team: James Smith, Jack Lund, Lucian Murphy

/// DOT PRODUCT -- Jack Lund ///

For my dot product feature I added a small HUD text element that tells the player how far they are from the exit at all times. I wanted it to feel like an actual gameplay aid instead of math hidden in the background, because the distance readout gives the player constant feedback on whether they are moving closer to the objective while exploring the house. I put this into the GameEnding script since that object already represents the exit and already has a reference to the player.

To calculate the value, I first create a vector from the player to the exit and then flatten the y value so it only measures distance across the floor. After that I use the dot product of the vector with itself, which gives the squared length of that vector. I then take the square root of that result to get the actual distance that is displayed on screen. I also made the UI text create itself at runtime if one is not already assigned, so the feature works without extra setup in the scene.

Vector3 toExit = transform.position - player.transform.position;
toExit.y = 0f;
float squaredDistanceToExit = Vector3.Dot(toExit, toExit);
float distanceToExit = Mathf.Sqrt(squaredDistanceToExit);
objectiveDistanceText.text = $"Exit: {distanceToExit:0.0} m";

To some this might seem like it would give the player an unfair advantage, but if you think about it, it just gives the player the straight line distance from them to the exit. If the exit is right behind the player and they have to go far away from it to eventually work their way back around then then this new element doesnt really help them out in a completely game breaking way.

The new/updated file for this is GameEnding.cs


/// LINEAR INTERPOLATION -- Lucian Murphy ///
I used linear interpolation to make the brightness of the gargoyles like lessen and increase in a pulsing manner, and did the same thing for the gargoyles sight as well, lengthening and shortening it as the pulse of the light got brighter and darker. For the ghost I used lerp to chose a random brightness for the ghost every frame to create a flickering effect for the ghost. for the Lerp function I used the book and wikipedia to help me build it, and used Gemini to help me debug my code and figure out how to make my script attach to the entity I was working on.

The idea for the pulse is the color starts out as off and the length of the sight is 0, and it increases by Time.deltaTime/2 each frame, which allows it to slowly increase until it hits f = 1, which then it decrease in the same manner until f = 0, and it repeats that process. 

The ghost flicker is less intuitive because it just picks a random number between 0 and 1 and that ends up being the brightness of the ghost light for that frame.

The new files for these are Ghost_Flicker.cs, Light_Increase.cs, and Gargoyle_sight_change.cs


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
