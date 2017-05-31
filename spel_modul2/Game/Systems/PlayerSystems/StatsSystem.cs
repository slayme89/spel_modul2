using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;

namespace Game.Systems
{
    public class StatsSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            //Update all stats for entities with StatsComponent
            foreach (var entity in cm.GetComponentsOfType<StatsComponent>())
            {
                StatsComponent stats = (StatsComponent)entity.Value;
                //See if there is any stats to remove
                if (stats.RemoveStats > 0)
                    UpdateEntityStatsFromHistory(entity.Key);

                //see if there is any stats to gain
                if (stats.AddStr > 0)
                    UpdateEntityStrength(entity.Key);
                if (stats.AddAgi > 0)
                    UpdateEntityAgillity(entity.Key);
                if (stats.AddSta > 0)
                    UpdateEntityStamina(entity.Key);
                if (stats.AddInt > 0)
                    UpdateEntityIntellect(entity.Key);
            }
        }

        //Update stats according to the history (if player died)
        private void UpdateEntityStatsFromHistory(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent comp = cm.GetComponentForEntity<StatsComponent>(entity);
            LevelComponent lvlComp = cm.GetComponentForEntity<LevelComponent>(entity);

            while (comp.RemoveStats > 0)
            {
                if (lvlComp.CurrentLevel <= 0)
                    break;

                if (comp.SpendableStats > 0)
                {
                    comp.SpendableStats -= 1;
                    comp.RemoveStats -= 1;
                }
                else
                {
                    if (comp.StatHistory.Length >= 3)
                    {
                        int start = comp.StatHistory.Length - 3;
                        string stat = comp.StatHistory.Substring(start);
                        switch (stat)
                        {
                            case "str":
                                if (cm.HasEntityComponent<AttackComponent>(entity) && cm.HasEntityComponent<HealthComponent>(entity))
                                {
                                    AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
                                    HealthComponent healthComp = cm.GetComponentForEntity<HealthComponent>(entity);
                                    attackComp.Damage = attackComp.Damage - 2;
                                    healthComp.Max = healthComp.Max - 1;
                                    healthComp.Current = healthComp.Current - 1;
                                    comp.Strength -= 1;
                                }
                                break;

                            case "agi":
                                if (cm.HasEntityComponent<MoveComponent>(entity) && cm.HasEntityComponent<AttackComponent>(entity))
                                {
                                    MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity);
                                    AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
                                    attackComp.RateOfFire = attackComp.RateOfFire - 0.05f;
                                    moveComp.Speed = moveComp.Speed - 0.03f;
                                    comp.Agility -= 1;
                                }
                                break;

                            case "sta":
                                if (cm.HasEntityComponent<HealthComponent>(entity))
                                {
                                    HealthComponent healthComp = cm.GetComponentForEntity<HealthComponent>(entity);
                                    healthComp.Max = healthComp.Max - 2;
                                    healthComp.Current = healthComp.Current - 2;
                                    comp.Stamina -= 1;
                                }
                                break;

                            case "int":
                                if (cm.HasEntityComponent<EnergyComponent>(entity))
                                {
                                    EnergyComponent energyComp = cm.GetComponentForEntity<EnergyComponent>(entity);
                                    energyComp.Max = energyComp.Max - 2;
                                    comp.Intellect -= 1;
                                }
                                break;
                        }
                        comp.StatHistory = comp.StatHistory.Substring(0, comp.StatHistory.Length - 3);
                        comp.RemoveStats -= 1;
                    }
                }
            }

        }

        //Add Strength
        private void UpdateEntityStrength(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if (cm.HasEntityComponent<AttackComponent>(entity) && cm.HasEntityComponent<HealthComponent>(entity))
            {
                StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
                AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
                HealthComponent healthComp = cm.GetComponentForEntity<HealthComponent>(entity);
                attackComp.Damage = attackComp.Damage + (2 * statComp.AddStr);
                healthComp.Max = healthComp.Max + (1 * statComp.AddStr);

                for (int i = 0; i <= statComp.AddStr - 1; i++)
                    statComp.StatHistory += "str";

                statComp.Strength += statComp.AddStr;
                statComp.AddStr = 0;
            }
        }
        //Add Agillity
        private void UpdateEntityAgillity(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if (cm.HasEntityComponent<MoveComponent>(entity) && cm.HasEntityComponent<AttackComponent>(entity))
            {
                StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
                MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity);
                AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
                attackComp.RateOfFire = attackComp.RateOfFire - (0.001f * statComp.AddAgi);
                moveComp.Speed = moveComp.Speed + (0.001f * statComp.AddAgi);

                for (int i = 0; i <= statComp.AddAgi - 1; i++)
                    statComp.StatHistory += "agi";

                statComp.Agility += statComp.AddAgi;
                statComp.AddAgi = 0;
            }
        }

        //Add Stamina
        private void UpdateEntityStamina(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if (cm.HasEntityComponent<HealthComponent>(entity))
            {
                StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
                HealthComponent healthComp = cm.GetComponentForEntity<HealthComponent>(entity);
                healthComp.Max = healthComp.Max + (2 * statComp.AddSta);

                for (int i = 0; i <= statComp.AddSta - 1; i++)
                    statComp.StatHistory += "sta";

                statComp.Stamina += statComp.AddSta;
                statComp.AddSta = 0;
            }
        }
        //Add Intellect
        private void UpdateEntityIntellect(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if (cm.HasEntityComponent<EnergyComponent>(entity))
            {
                StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
                EnergyComponent energyComp = cm.GetComponentForEntity<EnergyComponent>(entity);
                energyComp.Max = energyComp.Max + (2 * statComp.AddInt);

                for (int i = 0; i <= statComp.AddInt - 1; i++)
                    statComp.StatHistory += "int";

                statComp.Intellect += statComp.AddInt;
                statComp.AddInt = 0;
            }
        }
    }
}
