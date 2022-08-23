using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour{
    
    public Sound[] sounds;
        
    void Awake(){
       foreach(Sound s in sounds) {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
       }
    }

    public void PlaySound(string name){
        Sound s = Array.Find(sounds, sound=>sound.soundName==name);
        s.source.Play();
    }
}
