using Bunit;
using Xunit;
using BlazorApp1.Components.Pages;

namespace TestBlazor;

public class CounterTests : TestContext
{
    [Fact]
    public void SumarButton_ComputaLaSumaYLaMuestra()
    {
        // Render the Counter component (which in this repo is a simple sumador)
        var cut = RenderComponent<Counter>();

        // Fill inputs and click the button
        cut.Find("#aInput").Change("3");
        cut.Find("#bInput").Change("4");
        cut.Find("button").Click();

        // Verify the result is displayed
        Assert.Contains("Resultado: 7", cut.Markup);
    }
}