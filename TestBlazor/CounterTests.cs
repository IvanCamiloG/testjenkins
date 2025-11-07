using Bunit;
using Xunit;
using System;

// Adjust namespace/usings to match the Blazor app's component namespace
using BlazorApp1.Components.Pages;

public class CounterTests : TestContext
{
    [Fact]
    public void SumarButton_ComputaLaSumaYLaMuestra()
    {
        // Renderiza el componente Counter (tu versión es un sumador con inputs #aInput y #bInput)
        var cut = RenderComponent<Counter>();

        // Encuentra inputs por id y asigna valores
        var aInput = cut.Find("#aInput");
        var bInput = cut.Find("#bInput");

        aInput.Change("3");
        bInput.Change("4");

        // Haz clic en el botón
        cut.Find("button").Click();

        // Comprueba que el resultado esperado aparece en el marcado
        Assert.Contains("Resultado: 7", cut.Markup);
    }
}
