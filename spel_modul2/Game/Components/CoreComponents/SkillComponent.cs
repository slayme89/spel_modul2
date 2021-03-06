﻿using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game.Components
{
    public class SkillComponent : IComponent, ActionBarSlotComponent
    {
        public int EnergyCost { get; set; }
        public List<int> UsingEntities = new List<int>();
        public string IconFileName { get; set; }
        public Texture2D SkillIcon { get; set; }
        public Action Use { get; set; }
        public bool IsItem { get; set; } = false;

        public SkillComponent(Action action, int energyCost, string iconFileName)
        {
            EnergyCost = energyCost;
            Use = action;
            IconFileName = iconFileName;
        }

        public object Clone()
        {
            SkillComponent o = (SkillComponent)MemberwiseClone();
            o.UsingEntities = new List<int>(UsingEntities);
            o.IconFileName = string.Copy(IconFileName);
            return o;
        }
    }
}
