public class AlumnoViewModel
{
  private string _boleta;
  private string _nombre;
  private int _calificacion;

  public AlumnoViewModel(string boleta, string nombre, int calificacion)
  {
    _boleta = boleta;
    _nombre = nombre;
    _calificacion = calificacion;
  }

  public string Boleta { get { return _boleta; } set { _boleta = value; } }
  public string Nombre { get { return _nombre; } set { _nombre = value; } }
  public int Calificacion { get { return _calificacion; } set { _calificacion = value; } }
}
