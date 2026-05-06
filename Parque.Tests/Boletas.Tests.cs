using parque.Models;
using parque.Services;

namespace Parque.Tests;

[TestClass]
public class BoletaTests
{
    [TestMethod]
    public void EstaVigente_BoletaConFechaFutura_RetornaTrue()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(10), 500);
        Assert.IsTrue(boleta.EstaVigente());
    }

    [TestMethod]
    public void EstaVigente_BoletaVencida_RetornaFalse()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddSeconds(5), 500);
        Assert.IsTrue(boleta.EstaVigente());
    }

    [TestMethod]
    public void Precio_PrecioValido_SeAsignaCorrectamente()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 1000);
        Assert.AreEqual(1000f, boleta.Precio);
    }

    [TestMethod]
    public void Precio_PrecioCero_LanzaExcepcion()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            new BoletaGeneral(DateTime.Now.AddMinutes(5), 0));

        Assert.AreEqual("El precio debe ser mayor a cero.", ex.Message);
    }

    [TestMethod]
    public void Precio_PrecioNegativo_LanzaExcepcion()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            new BoletaGeneral(DateTime.Now.AddMinutes(5), -100));

        Assert.AreEqual("El precio debe ser mayor a cero.", ex.Message);
    }

    [TestMethod]
    public void FechaVencimiento_FechaPasada_LanzaExcepcion()
    {
        var ex = Assert.ThrowsException<ArgumentException>(() =>
            new BoletaGeneral(DateTime.Now.AddMinutes(-1), 500));

        Assert.AreEqual("La fecha de vencimiento debe ser futura.", ex.Message);
    }

    [TestMethod]
    public void Anular_BoletaActiva_CambiaEstadoAAnulada()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.Anular();
        Assert.IsTrue(boleta.EstaAnulada());
    }

    [TestMethod]
    public void Anular_BoletaYaAnulada_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.Anular();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => boleta.Anular());
        Assert.AreEqual("La boleta ya está anulada.", ex.Message);
    }

    [TestMethod]
    public void Anular_BoletaUsada_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.MarcarComoUsada();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => boleta.Anular());
        Assert.AreEqual("No se puede anular una boleta ya usada.", ex.Message);
    }

    [TestMethod]
    public void MarcarComoUsada_BoletaActiva_CambiaEstadoAUsada()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.MarcarComoUsada();
        Assert.IsTrue(boleta.EstaUsada());
    }

    [TestMethod]
    public void PuedeIngresar_BoletaActivaVigente_RetornaTrue()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        Assert.IsTrue(boleta.PuedeIngresar());
    }

    [TestMethod]
    public void PuedeIngresar_BoletaAnulada_RetornaFalse()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.Anular();
        Assert.IsFalse(boleta.PuedeIngresar());
    }

    [TestMethod]
    public void PuedeIngresar_BoletaUsada_RetornaFalse()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.MarcarComoUsada();
        Assert.IsFalse(boleta.PuedeIngresar());
    }

    [TestMethod]
    public void ObtenerTipoAcceso_BoletaGeneral_RetornaNormal()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        Assert.AreEqual(TipoAcceso.Normal, boleta.ObtenerTipoAcceso());
    }

    [TestMethod]
    public void ObtenerTipoAcceso_BoletaVIP_RetornaSinFila()
    {
        var boleta = new BoletaVIP(DateTime.Now.AddMinutes(5), 1200);
        Assert.AreEqual(TipoAcceso.SinFila, boleta.ObtenerTipoAcceso());
    }
}