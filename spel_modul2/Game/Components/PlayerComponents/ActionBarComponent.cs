﻿using System;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace Game.Components
{
    public delegate void Action(int entity, int position);

    public class ActionBarComponent : IComponent
    {
        public Point PositionOnScreen { get; set; }
        public Point SlotSize { get; set; }
        public ActionBarSlotComponent[] Slots { get; set; }

        public ActionBarComponent()
        {
            Slots = new ActionBarSlotComponent[4];
            SlotSize = new Point(30, 30);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
