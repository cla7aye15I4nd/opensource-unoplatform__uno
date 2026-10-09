using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Private.Infrastructure;
using Uno.UI.RuntimeTests.Helpers;

namespace Uno.UI.RuntimeTests.Tests.Windows_UI_Xaml_Controls;

[TestClass]
[RunsOnUIThread]
public class Given_BreadcrumbBar
{
	[TestMethod]
	public async Task When_Items_Arranged_SizeOfSet_Is_Readable()
	{
		var bar = new BreadcrumbBar { ItemsSource = new[] { "Home", "Library", "Buttons" } };

		await UITestHelper.Load(bar);

		var items = Descendants(bar).OfType<BreadcrumbBarItem>().Where(i => AutomationProperties.GetPositionInSet(i) > 0).ToArray();
		Assert.AreEqual(3, items.Length);
		Assert.IsTrue(items.All(i => AutomationProperties.GetSizeOfSet(i) == 3));
	}

	private static IEnumerable<DependencyObject> Descendants(DependencyObject parent)
	{
		for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
		{
			var child = VisualTreeHelper.GetChild(parent, i);
			yield return child;
			foreach (var descendant in Descendants(child))
			{
				yield return descendant;
			}
		}
	}
}
