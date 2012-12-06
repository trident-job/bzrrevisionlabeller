using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Remote;

namespace CcNet.Labeller.Tests
{
	[TestFixture]
	public class DateSpecifications : Specification
	{
		private BzrRevisionLabellerStub _labeller;
		private IIntegrationResult _previousResult;
		private string _label;

		protected override void Arrange()
		{
			FakeSystemClock fakeSystemClock = new FakeSystemClock(new DateTime(2010, 9, 21, 5, 13, 59));
			_previousResult = Mockery.DynamicMock<IIntegrationResult>();

			using (_mockery.Record())
			{
				Expect.Call(_previousResult.Label).Return("1.0.31.0");
				SetupResult.For(_previousResult.LastIntegrationStatus).Return(IntegrationStatus.Success);
			}

			_mockery.ReplayAll();

            DateTime myDateTime = new DateTime (2010,8,20,0,0,0);
            _labeller = new BzrRevisionLabellerStub(fakeSystemClock)
				{
                    // Use system date instead of hard coded string date !
                    StartDate = myDateTime.ToShortDateString(),
					Pattern = "1.0.{date}.0"
				};
			_labeller.SetRevision(0);
		}

		protected override void Act()
		{
			_label = _labeller.Generate(_previousResult);
		}

		[Test]
		public void TheElapsedDaysShouldBeCalculatedCorrectly()
		{
			Assert.That(_label, Is.EqualTo("1.0.32.0"));
		}
	}
}