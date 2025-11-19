namespace BE.Response
{
    public class ApiResulInfo
    {
        public string Message1 { get; set; } = "Bienvenido a mi API 😎";
        public string Fecha1 { get; set; }
        public string Version1 { get; set; }
        public int? Codigo { get; set; }
        public double? Valor { get; set; } = 99.99;
        public bool? Exitoso { get; set; }
        public List<string> Etiquetas { get; set; }
        public List<DatosExtra> InfoExtra { get; set; }
        public DateTime? fechaActaul { get; set; }
    }

    public class DatosExtra
    {
        public string Autor { get; set; }
        public string Descripcion { get; set; }
        public string Libros { get; set; } 
        public int Nivel { get; set; }
    }
}
