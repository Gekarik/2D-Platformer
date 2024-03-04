using System;

public interface ICollectible
{
    public event Action<ICollectible> Collected;

    void RespondToCollection();
}