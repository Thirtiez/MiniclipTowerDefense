using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomSFX : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] clips;

        [SerializeField]
        private bool playOnAwake;

        private AudioSource source;

        protected void Awake()
        {
            source = GetComponent<AudioSource>();

            if (playOnAwake)
            {
                PlayRandomClip();
            }
        }

        public void PlayRandomClip()
        {
            var clip = clips[Random.Range(0, clips.Length)];
            source?.PlayOneShot(clip);
        }
    }
}