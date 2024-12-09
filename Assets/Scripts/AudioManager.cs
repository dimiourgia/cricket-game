using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton pattern

    private AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;   // Background music clip
    public AudioClip buttonClickSound;
    public AudioClip outSound;// Sound for right mouse click

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        // Initialize AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        // Start playing background music
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // Loop the music
            audioSource.Play();
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // Play sound effect
        }
    }

    // New method to check right mouse button click
    public void PlayBallHitSound()
    {
        Debug.Log("Right mouse button clicked!"); // Debug message
        PlaySoundEffect(buttonClickSound); // Play sound effect for the right click
    }

    public void CheckLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Mouse Button Clicked");
            PlaySoundEffect(outSound);
        }
    }

}