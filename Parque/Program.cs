using parque.Services;
using parque.UI;

var servicio = new ParqueService();

var menu = new MenuConsola(servicio, servicio, servicio);
menu.Ejecutar();