public class EstadisticasViewModel
{
  private AlumnoViewModel[] _alumnos;
  public AlumnoViewModel[] Alumnos { get { return _alumnos; } }

  public EstadisticasViewModel(AlumnoViewModel[] alumnos)
  {
    _alumnos = alumnos;
  }

  public int NumAprobados()
  {
    return Array.FindAll(_alumnos, a => a.Calificacion >= 6).Length;
  }
  public int NumReprobados()
  {
    return Array.FindAll(_alumnos, a => a.Calificacion < 6).Length;
  }
  public float Promedio()
  {
    int suma = 0;
    Array.ForEach(_alumnos, a => suma += a.Calificacion);
    return _alumnos.Length == 0 ? 1 : suma * 1.0f / _alumnos.Length;
  }
  public int CalificacionMinima()
  {
    int minima = _alumnos.Length == 0 ? 1 : 10;//Si no hay por defecto será 0, si hay una empieza desde 10
    Array.ForEach(_alumnos, a => minima = a.Calificacion < minima ? a.Calificacion : minima);
    return minima;
  }
  public int CalificacionMaxima()
  {
    int maxima = 1;//Siempre empieza en 1
    Array.ForEach(_alumnos, a => maxima = a.Calificacion > maxima ? a.Calificacion : maxima);
    return maxima;
  }
}
