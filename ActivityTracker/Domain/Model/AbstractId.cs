namespace ActivityTracker.Domain.Model
{
	public abstract class AbstractId
	{
		protected AbstractId(string id)
		{
			Id = id;
		}

		public string Id { get; }

		public override string ToString()
		{
			return Id;
		}
	}
}
