using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Managers;

namespace GameEngine.Systems
{
    class QuestSystem : ISystem
    {
        private QuestComponent[] ActiveQuestList = new QuestComponent[3];


        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            int i = 0;
            ActiveQuestList = null;
            ActiveQuestList = new QuestComponent[3];

            foreach(var questEntity in cm.GetComponentsOfType<QuestComponent>())
            {
                QuestComponent quest = (QuestComponent)questEntity.Value;
                if (quest.IsActive)
                {
                    ActiveQuestList[i] = quest;
                }
            }




        }
    }
}
