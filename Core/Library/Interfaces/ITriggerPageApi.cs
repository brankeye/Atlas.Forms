namespace Atlas.Forms.Interfaces
{
    public interface ITriggerPageApi
    {
        ITargetPageApi Appears();

        ITargetPageApi IsCreated();
    }
}
