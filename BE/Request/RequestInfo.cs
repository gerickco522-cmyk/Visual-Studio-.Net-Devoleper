namespace BE.Request
{
    public class RequestInfo
    {
            public string Usuario { get; set; }
            public int Edad { get; set; } = 1;
            public bool? MostrarFechaFutura { get; set; }
            public int? NotaUsuario { get; set; } = 5;
    }
}
