using UnityEngine;

public class RandomAudioClips : MonoBehaviour
{
    public AudioClip[] clips;
    public Vector2 minMaxTime = Vector2.one;

    public AudioSource audioSource;
    private float timer;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        timer = GetRandomTimerInterval();
    }

    void Update()
    {
        HandleIntervalTimer();
    }

    private void HandleIntervalTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = GetRandomTimerInterval();
            PlayRandomAudio();
        }
    }

    private float GetRandomTimerInterval()
    {
        return Random.Range(minMaxTime.x, minMaxTime.y);
    }

    private void PlayRandomAudio()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]); 
    }
}
