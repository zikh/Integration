namespace Microsoft.Samples.BizTalk.GenericAdapter.Contracts
{
    public interface IInboundQueue<T>
    {
        T Dequeue();
        void Enqueue(T message);
    }
}
