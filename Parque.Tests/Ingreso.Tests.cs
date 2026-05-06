using parque.Models;
using parque.Services;

namespace Parque.Tests;

[TestClass]
public class IngresoTests
{
    [TestMethod]
    public void Constructor_BoletaValidaYAtraccion_RegistraIngresoCorrectamente()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var atraccion = new Atraccion("Rueda de Chicago");

        var ingreso = new Ingreso(boleta, atraccion);

        Assert.AreEqual(atraccion, ingreso.Atraccion);
        Assert.AreEqual(boleta, ingreso.Boleta);
        Assert.AreEqual(TipoAcceso.Normal, ingreso.TipoAcceso);
    }

    [TestMethod]
    public void Constructor_BoletaValida_MarcaBoletaComoUsada()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var atraccion = new Atraccion("Carrusel");

        new Ingreso(boleta, atraccion);

        Assert.IsTrue(boleta.EstaUsada());
    }

    [TestMethod]
    public void Constructor_BoletaVIP_TipoAccesoEsSinFila()
    {
        var boleta = new BoletaVIP(DateTime.Now.AddMinutes(5), 1200);
        var atraccion = new Atraccion("Tobogán");

        var ingreso = new Ingreso(boleta, atraccion);

        Assert.AreEqual(TipoAcceso.SinFila, ingreso.TipoAcceso);
    }

    [TestMethod]
    public void Constructor_BoletaAnulada_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.Anular();
        var atraccion = new Atraccion("Carrusel");

        var ex = Assert.ThrowsException<InvalidOperationException>(() =>
            new Ingreso(boleta, atraccion));

        Assert.AreEqual("La boleta no es válida para ingresar.", ex.Message);
    }

    [TestMethod]
    public void Constructor_BoletaYaUsada_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var atraccion = new Atraccion("Carrusel");
        new Ingreso(boleta, atraccion);

        Assert.ThrowsException<InvalidOperationException>(() =>
            new Ingreso(boleta, atraccion));
    }

    [TestMethod]
    public void Constructor_HoraIngreso_EsCercanaAHoraActual()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var atraccion = new Atraccion("Carrusel");

        var antes = DateTime.Now;
        var ingreso = new Ingreso(boleta, atraccion);
        var despues = DateTime.Now;

        Assert.IsTrue(ingreso.Hora >= antes && ingreso.Hora <= despues);
    }
}