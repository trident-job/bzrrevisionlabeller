namespace CcNet.Labeller.Tests
{
	public class BzrRevisionLabellerStub : SvnRevisionLabeller
	{
		public BzrRevisionLabellerStub()
		{
		}

		public BzrRevisionLabellerStub(ISystemClock systemClock) : base(systemClock)
		{
		}

		public void SetRevision(int svnRevision)
		{
			_revision = svnRevision;
		}

		protected override int GetRevision()
		{
			return _revision;
		}

		private int _revision;
	}
}