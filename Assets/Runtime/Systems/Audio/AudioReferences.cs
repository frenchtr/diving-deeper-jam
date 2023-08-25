using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect {

    public AudioClip[] clip;
    [Header("Parameters")]
    public float volume = 1;
    public float minPitch = 1, maxPitch = 1;
    public bool sequential = false, overlap = true, loop = false;

    internal AudioSource source;

    private int clipIndex;

    /// <summary> Ensures host has an AudioSource (Must be called in Start) </summary>
    public void Init(GameObject host, AudioReferences references) {

        if (clip.Length == 0) {
            Debug.LogError($"Sound Effect on \"{host.name}\" doesn't haven't any audio clips!");
            return;
        }

        source = host.AddComponent<AudioSource>();

        foreach (var s in host.GetComponents<AudioSource>())
            if (s.clip == null) source = s;
        if (source == null) source = host.AddComponent<AudioSource>();

        source.outputAudioMixerGroup = references.SoundMixerGroup;
    }

    /// <summary> Play the sound effect. </summary>
    public void Play() {

        // return if overlapping matters
        if (!overlap && source.isPlaying) return;

        // get clip
        AudioClip currentClip;
        if (sequential) {
            currentClip = clip[clipIndex % clip.Length];
            clipIndex++;
        } else currentClip = clip[Random.Range(0, clip.Length - 1)];

        // play sound
        source.loop = loop;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.volume = volume;

        if (loop) {
            source.clip = currentClip;
            source.Play();
        }
        else source.PlayOneShot(currentClip);
    }

    /// <summary> Stop the sound effect. </summary>
    public void Stop() {
        source.Stop();
    }
}

[CreateAssetMenu(fileName = "Audio References", menuName = "Scriptables/Systems/Audio References")]
public class AudioReferences : ScriptableObject {

    [SerializeField] private float masterVolume, musicVolume, soundVolume;

    [SerializeField] private AudioMixerGroup musicGroup, soundGroup;
    [SerializeField] private string masterVolumeParameter, musicVolumeParameter, soundVolumeParameter;
    [SerializeField] private float maxVolume;

    public AudioMixerGroup SoundMixerGroup => soundGroup;

    public void Update() {

        void SetVolume(string name, float volume) => musicGroup.audioMixer.SetFloat(name, volume == 0 ? -80f : Mathf.Log10(volume) * maxVolume);

        SetVolume(masterVolumeParameter, masterVolume);
        SetVolume(musicVolumeParameter,  musicVolume);
        SetVolume(soundVolumeParameter,  soundVolume);
    }
}
