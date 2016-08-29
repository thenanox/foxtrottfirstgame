using UnityEngine;
using System.Collections;

public class AudioManager : SingletonComponent<AudioManager>
{
    private AudioSource source;

    public GameObject gameSoundManager;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void startGame()
    {
        GameObject sound = GameObject.Instantiate(gameSoundManager);
        source.Stop();

        DontDestroyOnLoad(sound);
    }
}
