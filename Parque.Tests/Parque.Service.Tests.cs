using parque.Models;
using parque.Services;

namespace Parque.Tests;

[TestClass]
public class ParqueServiceTests
{
    private ParqueService _servicio = null!;

    [TestInitialize]
    public void Setup()
    {
        _servicio = new ParqueService();
    }

    [TestMethod]
    public void AgregarAtraccion_AtraccionValida_SeAgregaCorrectamente()
    {
        _servicio.AgregarAtraccion(new Atraccion("Montaña Rusa"));
        Assert.AreEqual(1, _servicio.ObtenerAtracciones().Count);
    }

    [TestMethod]
    public void AgregarAtraccion_Null_LanzaExcepcion()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
            _servicio.AgregarAtraccion(null!));
    }

    [TestMethod]
    public void ObtenerAtracciones_SinAtracciones_RetornaListaVacia()
    {
        Assert.AreEqual(0, _servicio.ObtenerAtracciones().Count);
    }

    [TestMethod]
    public void ObtenerAtracciones_VariasAtracciones_RetornaTodasCorrectamente()
    {
        _servicio.AgregarAtraccion(new Atraccion("Montaña Rusa"));
        _servicio.AgregarAtraccion(new Atraccion("Carrusel"));
        _servicio.AgregarAtraccion(new Atraccion("Tobogán"));

        Assert.AreEqual(3, _servicio.ObtenerAtracciones().Count);
    }

    [TestMethod]
    public void VenderBoleta_BoletaValida_RetornaLaMismaBoleta()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var resultado = _servicio.VenderBoleta(boleta);

        Assert.AreEqual(boleta, resultado);
    }

    [TestMethod]
    public void VenderBoleta_Null_LanzaExcepcion()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
            _servicio.VenderBoleta(null!));
    }

    [TestMethod]
    public void RegistrarIngreso_BoletaYAtraccionValidas_RetornaIngreso()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var atraccion = new Atraccion("Carrusel");

        var ingreso = _servicio.RegistrarIngreso(boleta, atraccion);

        Assert.IsNotNull(ingreso);
        Assert.AreEqual(atraccion, ingreso.Atraccion);
    }

    [TestMethod]
    public void RegistrarIngreso_BoletaNull_LanzaExcepcion()
    {
        var atraccion = new Atraccion("Carrusel");

        Assert.ThrowsException<ArgumentNullException>(() =>
            _servicio.RegistrarIngreso(null!, atraccion));
    }

    [TestMethod]
    public void RegistrarIngreso_AtraccionNull_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);

        Assert.ThrowsException<ArgumentNullException>(() =>
            _servicio.RegistrarIngreso(boleta, null!));
    }

    [TestMethod]
    public void RegistrarIngreso_BoletaAnulada_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        boleta.Anular();
        var atraccion = new Atraccion("Carrusel");

        Assert.ThrowsException<InvalidOperationException>(() =>
            _servicio.RegistrarIngreso(boleta, atraccion));
    }

    [TestMethod]
    public void RegistrarIngreso_BoletaUsadaDosVeces_LanzaExcepcion()
    {
        var boleta = new BoletaGeneral(DateTime.Now.AddMinutes(5), 500);
        var atraccion = new Atraccion("Carrusel");

        _servicio.RegistrarIngreso(boleta, atraccion);

        Assert.ThrowsException<InvalidOperationException>(() =>
            _servicio.RegistrarIngreso(boleta, atraccion));
    }
}