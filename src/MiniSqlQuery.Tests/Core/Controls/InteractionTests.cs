using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MiniSqlQuery.Core.Controls
{
	[TestFixture(TestOf = typeof(Interaction))]
	public class InteractionTests
	{
		[Test, Apartment(ApartmentState.STA)]
		public void When_User_Presses_Cancel_Empty_String_Is_Returned()
		{
			var result = Interaction.InputBox("");
			Assert.That(result, Is.Not.Null.And.Empty);
		}
	}
}