using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource seAudioSource;
        [SerializeField] private AudioClip[] seAudioClips;

        public static AudioManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySe(SePath sePath)
        {
            int index = (int)sePath;
            if (index >= 0 && index < seAudioClips.Length)
            {
                seAudioSource.PlayOneShot(seAudioClips[index]);
            }
            else
            {
                Debug.LogWarning("Sound index out of range.");
            }
        }

        public void SetVolume(float volume)
        {
            seAudioSource.volume = volume;
        }

        public enum SePath
        {
            SOBA_HIT,
        }
    }
}