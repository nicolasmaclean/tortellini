using Gummi.Data;
using Gummi.Utility;
using UnityEngine;

namespace Game.Utility.Audio
{
    [CreateAssetMenu(menuName = "Audio Clip")]
    public class SFXData : ScriptableObject
    {
        public AudioClip Clip;
        public bool Loop;

        [Range(0, 1)] public float Volume = 0.5f;
        [Range(-3, 3)] public float Pitch = 1f;

        public void Play() => PlayGO();
        public void Play(TransformData transform) => PlayGO(transform);
        
        public GameObject PlayGO()
        {
            if (!Clip) throw new System.ArgumentNullException(nameof(Clip));
            GameObject go = Pool.CheckOut<AudioSource>();
            AudioSource source = go.GetComponent<AudioSource>();
            
            source.Load(this);
            source.Play();

            if (!Loop)
            {
                Destroy(go, Clip.length);
            }

            return go;
        }

        public GameObject PlayGO(TransformData transform)
        {
            if (!Clip) throw new System.ArgumentNullException(nameof(Clip));
            GameObject go = Pool.CheckOut<AudioSource>();
            AudioSource source = go.GetComponent<AudioSource>();
            
            source.Load(this, transform);
            source.Play();

            if (!Loop)
            {
                Destroy(go, Clip.length);
            }

            return go;
        }
    }

    public static class AudioSourceExtensions
    {
        public static void Load(this AudioSource source, SFXData data)
        {
            source.LoadBase(data);
            source.spatialBlend = 0;
        }

        public static void Load(this AudioSource source, SFXData data, TransformData target)
        {
            source.LoadBase(data);
            source.spatialBlend = 1;

            var transform = source.transform;
            transform.position = target.Position;
            transform.rotation = target.Rotation;
            transform.localScale = target.LocalScale;
        }

        static void LoadBase(this AudioSource source, SFXData data)
        {
            source.clip = data.Clip;
            source.loop = data.Loop;
            source.volume = data.Volume;
            source.pitch = data.Pitch;
        }
    }
}