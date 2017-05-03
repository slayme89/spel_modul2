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
                UpdateEntityStrength(entity.Key);
                UpdateEntityAgillity(entity.Key);
                UpdateEntityStamina(entity.Key);
                UpdateEntityIntellect(entity.Key);
            }
        }
        
        //add 1 of the given statType to the entity
        public void AddStatToEntity(int entity, string statType)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);

            switch (statType)
            {
                case "str":
                    statComp.Strength += 1;
                    statComp.StatHistory += "str";
                    break;
                case "agi":
                    statComp.Agillity += 1;
                    statComp.StatHistory += "agi";
                    break;
                case "sta":
                    statComp.Stamina += 1;
                    statComp.StatHistory += "sta";
                    break;
                case "int":
                    statComp.Intellect += 1;
                    statComp.StatHistory += "int";
                    break;
            }
            
        }

        //Remove 1 of the given statType from entity
        public void RemoveStatFromEntity(int entity, string statType)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);

            switch (statType)
            {
                case "str":
                    statComp.Strength -= 1;
                    break;
                case "agi":
                    statComp.Agillity -= 1;
                    break;
                case "sta":
                    statComp.Stamina -= 1;
                    break;
                case "int":
                    statComp.Intellect -= 1;
                    break;
            }
            statComp.StatHistory.Substring(0, statComp.StatHistory.Length - 4);
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
