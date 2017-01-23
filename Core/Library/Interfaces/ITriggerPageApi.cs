namespace atlas.core.Library.Interfaces
{
    public interface ITriggerPageApi
    {
        ITargetPageApi Appears();

        ITargetPageApi Disappears();

        ITargetPageApi IsCreated();
    }
}
