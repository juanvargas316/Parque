using parque.Models;
using parque.Services;

namespace Parque.Tests;

[TestClass]
public class AtraccionTests
{
    [TestMethod]
    public void Constructor_NombreValido_SeAsignaCorrectamente()
    {
        var atraccion = new Atraccion("Montaña Rusa");
        Assert.AreEqual("Montaña Rusa", atraccion.Nombre);
    }

    [TestMethod]
    public void Constructor_NombreVacio_LanzaExcepcion()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            new Atraccion(""));

        Assert.AreEqual("El nombre no puede estar vacío.", ex.Message);
    }

    [TestMethod]
    public void Constructor_NombreEspacios_LanzaExcepcion()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            new Atraccion("   "));

        Assert.AreEqual("El nombre no puede estar vacío.", ex.Message);
    }

    [TestMethod]
    public void Constructor_NombreNull_LanzaExcepcion()
    {
        Assert.ThrowsException<ArgumentException>(() =>
            new Atraccion(null!));
    }
}