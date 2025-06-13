using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int lastTrackIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (musicTracks.Length > 0)
        {
            PlayRandomTrack();
        }
        else
        {
            Debug.LogWarning("No music tracks assigned!");
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying && musicTracks.Length > 0)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        int nextTrackIndex;

        // Prevent repeating the same track immediately
        do
        {
            nextTrackIndex = Random.Range(0, musicTracks.Length);
        } while (musicTracks.Length > 1 && nextTrackIndex == lastTrackIndex);

        lastTrackIndex = nextTrackIndex;
        audioSource.clip = musicTracks[nextTrackIndex];
        audioSource.Play();
    }
}
