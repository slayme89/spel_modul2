using Microsoft.Xna.Framework.Audio;
using GameEngine.Managers;
using System;

namespace GameEngine.Components
{
    public class SoundComponent : IComponent
    {
        public SoundEffectInstance WalkSound { get; set; }
        public string WalkFile { get; set; }
        public bool PlayWalkSound { get; set; }
        public SoundEffectInstance AttackSound { get; set; }
        public string AttackFile { get; set; }
        public bool PlayAttackSound { get; set; }
        public SoundEffectInstance DamageSound { get; set; }
        public string DamageFile { get; set; }
        public bool PlayDamageSound { get; set; }

        public SoundComponent(string walkfile, string attackFile, string damageFile)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            AttackFile = attackFile;
            WalkFile = walkfile;
            DamageFile = damageFile;
            PlayWalkSound = false;
            PlayAttackSound = false;
            PlayDamageSound = false;

            WalkSound = rm.GetResource<SoundEffect>(walkfile).CreateInstance();
            WalkSound.IsLooped = true;
            AttackSound = rm.GetResource<SoundEffect>(attackFile).CreateInstance();
            DamageSound = rm.GetResource<SoundEffect>(damageFile).CreateInstance();
            WalkSound.Volume *= 0.3f;
            AttackSound.Volume *= 0.3f;
            DamageSound.Volume *= 0.3f;
        }

        public object Clone()
        {
            ResourceManager rm = ResourceManager.GetInstance();
            SoundComponent o = (SoundComponent)MemberwiseClone();
            o.WalkSound = rm.GetResource<SoundEffect>(WalkFile).CreateInstance();
            o.WalkFile = string.Copy(WalkFile);
            o.AttackSound = rm.GetResource<SoundEffect>(AttackFile).CreateInstance();
            o.AttackFile = string.Copy(AttackFile);
            o.DamageSound = rm.GetResource<SoundEffect>(DamageFile).CreateInstance();
            o.DamageFile = string.Copy(DamageFile);
            o.WalkSound.Volume *= 0.3f;
            o.AttackSound.Volume *= 0.3f;
            o.DamageSound.Volume *= 0.3f;
            return o;
        }
    }
}
