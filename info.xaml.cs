using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
namespace Grupo1_tarea_2_1;

public partial class info : ContentPage
{
    Pin pin;
    public info(double lat, double lon,string pais,string cap,string pobla,string area)
	{

		InitializeComponent();

        var map = new Microsoft.Maui.Controls.Maps.Map(MapSpan.FromCenterAndRadius(new Location(lat, lon), Distance.FromMiles(1)));

        // Crear el pin cona ubicación actual
        pin = new Pin
        {
            Label = "Pais: " + pais+", Capital: " + cap,
            Address = "Poblacion: "+pobla+", Area: "+area,
            Type = PinType.Place,
            Location = new Location(lat, lon)
        };

        

        // Agregar el pin al mapa
        map.Pins.Add(pin);

        // Establecer el contenido de la página como el mapa
        Content = map;
    }
}