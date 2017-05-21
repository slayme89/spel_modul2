using System;

namespace GameEngine
{
    public class EntityTuple<T1, T2> : Tuple<T1, T2>
    {
        public int Entity;

        public EntityTuple(int entity, T1 item1, T2 item2) : base(item1, item2)
        {
            Entity = entity;
        }
    }

    public class EntityTuple<T1, T2, T3> : Tuple<T1, T2, T3>
    {
        public int Entity;

        public EntityTuple(int entity, T1 item1, T2 item2, T3 item3) : base(item1, item2, item3)
        {
            Entity = entity;
        }
    }

    public class EntityTuple
    {
        public static EntityTuple<T1, T2> Create<T1, T2>(int entity, T1 item1, T2 item2)
        {
            return new EntityTuple<T1, T2>(entity, item1, item2);
        }

        public static EntityTuple<T1, T2, T3> Create<T1, T2, T3>(int entity, T1 item1, T2 item2, T3 item3)
        {
            return new EntityTuple<T1, T2, T3>(entity, item1, item2, item3);
        }
    }
}