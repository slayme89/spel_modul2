namespace GameEngine.Components
{
    public interface ActionBarSlotComponent 
    {
        Action Use { get; set; }
        bool IsItem { get; set; }
    }
}
