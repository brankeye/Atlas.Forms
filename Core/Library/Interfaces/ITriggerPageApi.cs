namespace Atlas.Forms.Interfaces
{
    public interface ITriggerPageApi
    {
        ITargetPageApi Appears();

        ITargetPageApi Disappears();

        ITargetPageApi IsCreated();
    }
}
