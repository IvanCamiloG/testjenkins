using TestTUnit.Data;

namespace TestTUnit;

public class Tests
{
   

    [Test]
    [Arguments(1, 2,   3)]
    [Arguments(2, 3, 5)]
   public async Task sumaTest (int a, int b, int c)
    {
        Console.WriteLine("This one can accept arguments from an attribute");
        var calculadora = new calculadora();
        // Call Sumar with two arguments as defined in ConsoleApp1/calculadora.cs
        var result = calculadora.Sumar(a, b);

        await Assert.That(result).IsEqualTo(c);
    }

   
}
