using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E4.Models;
using System.Text.Json;

namespace E4.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    EstadisticasViewModel? evm = HttpContext.Request.Cookies["ETHVM"] == null ? null :
      JsonSerializer.Deserialize<EstadisticasViewModel>(HttpContext.Request.Cookies["ETHVM"]);
    if (evm == null)
    {
      //Cargamos los datos de la estadística
      List<AlumnoViewModel> alumnos = new List<AlumnoViewModel>();
      string[] insNombres = new string[]{"Brayan", "Britanny", "Kevin", "Brandon", "Maluma", "Donatello", "Da Vinci",
          "Copérnico", "Galileo", "Newton"};
      Random random = new Random(Guid.NewGuid().GetHashCode());
      for (byte alumnoIndx = 0; alumnoIndx < insNombres.Length; alumnoIndx++)
      {
        alumnos.Add(new AlumnoViewModel("2020" + alumnoIndx.ToString("000000"), insNombres[alumnoIndx], random.Next(10) + 1));
      }
      evm = new EstadisticasViewModel(alumnos.ToArray());

      //Galletaso
      CookieOptions cookieOptions = new CookieOptions();
      cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(1));
      HttpContext.Response.Cookies.Append("ETHVM", JsonSerializer.Serialize(evm), cookieOptions);
    }
    if (HttpMethods.IsPost(Request.Method))
    {
      Array.ForEach(evm.Alumnos, a => a.Calificacion = Convert.ToInt32(Request.Form[$"kali{a.Boleta}"].ToString()));
      int reprobados = evm.NumReprobados();
      int aprobados = evm.NumAprobados();
      ViewData["EstAprobados"] = (aprobados * 1.0 / (reprobados + aprobados) * 100).ToString("0.##") + "%";
      ViewData["EstReprobados"] = (reprobados * 1.0 / (reprobados + aprobados) * 100).ToString("0.##") + "%";
      ViewData["EstPromedio"] = evm.Promedio().ToString("0.##");
      ViewData["EstCalMin"] = evm.CalificacionMinima().ToString();
      ViewData["EstCalMax"] = evm.CalificacionMaxima().ToString();
    }
    return View(evm);
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
