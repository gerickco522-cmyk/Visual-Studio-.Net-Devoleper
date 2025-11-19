namespace BE.DTO
{
    public class Persona
    {
        // Campos privados (ENCAPSULADOS)
        private string _nombre;
        private int _edad;

        // Propiedad expuesta con validación
        public string Nombre
        {
            get => _nombre;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("El nombre es obligatorio.");

                _nombre = value;
            }
        }
        public int Edad
        {
            get => _edad;
            private set
            {
                if (value < 0)
                    throw new Exception("La edad no puede ser negativa.");

                _edad = value;
            }
        }
        public Persona(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }

        public string ObtenerDescripcion()
        {
            return $"La persona {Nombre} tiene {Edad} años.";
        }
    }
}
