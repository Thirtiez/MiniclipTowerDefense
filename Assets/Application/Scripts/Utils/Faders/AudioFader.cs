using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class AudioFader : BaseFader<AudioSource>
    {
        protected override float targetValue
        {
            get { return targetComponent.volume; }
        }

        public bool IsAudioClipPlaying
        {
            get { return targetComponent.isPlaying; }
        }

        protected override void UpdateTargetValue(float newVolume)
        {
            targetComponent.volume = Mathf.Clamp01(newVolume);
        }

        public void PlayAudioClip()
        {
            targetComponent.Play();
        }

        public void StopAudioClip()
        {
            targetComponent.Stop();
        }
    }
}