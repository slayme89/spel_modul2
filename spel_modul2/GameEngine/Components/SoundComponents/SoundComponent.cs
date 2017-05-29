using GameEngine.Managers;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public enum SoundAction
    {
        Play,
        Pause,
        Stop,
        None
    }

    public class SoundComponent : IComponent
    {
        public Dictionary<string, MappedValue> Sounds { get; set; }

        public SoundComponent(string[] typeOfSound, string[] filePathName)
        {
            ResourceManager rm = ResourceManager.GetInstance();
            Sounds = new Dictionary<string, MappedValue>();

            if (typeOfSound.Length != filePathName.Length)
                throw new Exception("Need to have matching filename to type of sound, check so that both arrays are of the same size");

            for (int i = 0; i < filePathName.Length; i++)
                Sounds.Add(typeOfSound[i], new MappedValue(rm.GetResource<SoundEffect>(filePathName[i]).CreateInstance(), false));
        }

        public SoundComponent(string[] typeOfSound ,string[] filePathName, bool[] isLooping)
        {
            ResourceManager rm = ResourceManager.GetInstance();
            Sounds = new Dictionary<string, MappedValue>();

            if (typeOfSound.Length != filePathName.Length || isLooping.Length != filePathName.Length)
                throw new Exception("Need to have matching filename to type of sound and islooping bool, check so that all arrays are of the same size");

            for (int i = 0; i < filePathName.Length; i++)
                Sounds.Add(typeOfSound[i], new MappedValue(rm.GetResource<SoundEffect>(filePathName[i]).CreateInstance(), isLooping[i]));
        }

        public object Clone()
        {
            SoundComponent o = (SoundComponent)MemberwiseClone();
            return o;
        }
    }

    public class MappedValue
    {
        public SoundEffectInstance Sound { get; set; }
        public bool Loop { get; set; } = false;
        public SoundAction Action { get; set; } = SoundAction.None;

        public MappedValue(SoundEffectInstance sound, bool isLooping)
        {
            Sound = sound;
            Loop = isLooping;
        }
    }
}
