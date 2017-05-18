using Microsoft.Xna.Framework.Audio;

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
            AttackFile = attackFile;
            WalkFile = walkfile;
            DamageFile = damageFile;
            PlayWalkSound = false;
            PlayAttackSound = false;
            PlayDamageSound = false;
        }
    }
}
