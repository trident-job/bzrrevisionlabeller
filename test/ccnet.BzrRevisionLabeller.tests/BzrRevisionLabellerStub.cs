namespace CcNet.Labeller.Tests
{
	public class BzrRevisionLabellerStub : BzrRevisionLabeller
	{
		public BzrRevisionLabellerStub()
		{
		}

		public BzrRevisionLabellerStub(ISystemClock systemClock) : base(systemClock)
		{
		}

		public void SetRevision(int bzrRevision)
		{
			_revision = bzrRevision;
		}

		protected override int GetRevision()
		{
			return _revision;
		}

		private int _revision;
	}
}