using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound{

    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;

    public string soundName;

    [Range(0f,1f)]
    public float volume;

}
