namespace DrawingBattleArena
{
    public interface IObserver
    {
        void Update(string eventType, object data);
    }
}
