using Microsoft.Xna.Framework;

namespace GameEngine
{
    class StatsSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            
            //Update all stats for entities with StatsComponent
            foreach (var entity in cm.GetComponentsOfType<StatsComponent>())
            {
                //Skall köras vid levelLost
                //UpdateEntityStats((StatsComponent)entity.Value);

                //Gain stats bonuses
                UpdateEntityStrength(entity.Key);
                UpdateEntityAgillity(entity.Key);
                UpdateEntityStamina(entity.Key);
                UpdateEntityIntellect(entity.Key);
            }
        }
        

        private void UpdateEntityStatsFromHistory(StatsComponent comp)
        {
            int numIterations = comp.StatHistory.Length / 3;
            comp.Agillity = 0;
            comp.Intellect = 0;
            comp.Stamina = 0;
            comp.Strength = 0;

            for (int i = 0; i <= numIterations; i++)
            {
                int start = 0;
                int end = 2;
                string stat = comp.StatHistory.Substring(start, end);

                switch (stat)
                {
                    case "str": comp.Strength += 1;  break;
                    case "int": comp.Intellect += 1; break;
                    case "agi": comp.Agillity += 1;  break;
                    case "sta": comp.Stamina += 1;   break;
                }
                start += 3;
                end += 3;
            }
        }


        private void UpdateEntityStrength(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
            AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
            HealthComponent healthComp = cm.GetComponentForEntity<HealthComponent>(entity);
            int dmg = attackComp.Damage + (2 * statComp.Strength);
            int health = healthComp.Max + (1 * statComp.Strength);

            attackComp.Damage = dmg;
            healthComp.Max = health;
        }

        private void UpdateEntityAgillity(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity);
            AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
            float fireRate = attackComp.RateOfFire + (0.05f * statComp.Agillity);
            float moveSpeed = moveComp.Speed + (0.03f * statComp.Agillity);

            attackComp.RateOfFire = fireRate;
            moveComp.Speed = moveSpeed;
        }

        private void UpdateEntityStamina(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
            HealthComponent healthComp = cm.GetComponentForEntity<HealthComponent>(entity);
            int health = healthComp.Max + (2 * statComp.Stamina);

            healthComp.Max = health;
        }

        private void UpdateEntityIntellect(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
            EnergyComponent energyComp = cm.GetComponentForEntity<EnergyComponent>(entity);
            int energy = energyComp.Max + (2 * statComp.Intellect);

            energyComp.Max = energy;
        }
    }
}
