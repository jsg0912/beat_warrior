namespace MyPooler
{
	public interface IPooledObject
	{
		void OnRequestedFromPool();
		void DiscardToPool();
	}
}



