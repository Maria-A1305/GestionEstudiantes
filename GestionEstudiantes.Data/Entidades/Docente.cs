using GestionEstudiantes.Datos.Comun;

namespace GestionEstudiantes.Datos.Entidades
{
    /// <summary>
    /// Clase que representa a un docente que puede impartir varias asignaturas
    /// </summary>
    public class Docente
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private Asignatura[] asignaturas;
        private int contadorAsignaturas;
        private const int MAX_ASIGNATURAS = 5;

        public Docente(string cedula, string nombre, string apellido)
        {
            Cedula = cedula;
            Nombre = nombre;
            Apellido = apellido;
            asignaturas = new Asignatura[MAX_ASIGNATURAS];
            contadorAsignaturas = 0;
        }

        /// <summary>
        /// Agrega una asignatura al docente
        /// </summary>
        public OperationResult AgregarAsignatura(Asignatura asignatura)
        {
            try
            {
                if (asignatura == null)
                {
                    return new OperationResult(false, "La asignatura no puede ser nula.");
                }

                if (contadorAsignaturas >= MAX_ASIGNATURAS)
                {
                    return new OperationResult(false, "El docente ha alcanzado el máximo de asignaturas permitidas.");
                }

                // Verificar si el código de la asignatura ya existe
                for (int i = 0; i < contadorAsignaturas; i++)
                {
                    if (asignaturas[i].Codigo.Equals(asignatura.Codigo, StringComparison.OrdinalIgnoreCase))
                    {
                        return new OperationResult(false, "Ya existe una asignatura con ese código.");
                    }
                }

                asignaturas[contadorAsignaturas] = asignatura;
                contadorAsignaturas++;
                return new OperationResult(true, "Asignatura agregada exitosamente.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al agregar asignatura: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca una asignatura por código
        /// </summary>
        public OperationResult BuscarAsignatura(string codigo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    return new OperationResult(false, "El código de la asignatura no puede estar vacío.");
                }

                for (int i = 0; i < contadorAsignaturas; i++)
                {
                    if (asignaturas[i].Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    {
                        return new OperationResult(true, "Asignatura encontrada.", asignaturas[i]);
                    }
                }

                return new OperationResult(false, "Asignatura no encontrada.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al buscar asignatura: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el listado de asignaturas del docente
        /// </summary>
        public string ObtenerListadoAsignaturas()
        {
            if (contadorAsignaturas == 0)
            {
                return "No hay asignaturas registradas.";
            }

            string listado = $"\nAsignaturas del docente {Nombre} {Apellido}:\n";
            for (int i = 0; i < contadorAsignaturas; i++)
            {
                listado += $"{i + 1}. {asignaturas[i].MostrarInformacion()}\n";
            }

            return listado;
        }

        /// <summary>
        /// Obtiene el número de asignaturas del docente
        /// </summary>
        public int ObtenerNumeroAsignaturas()
        {
            return contadorAsignaturas;
        }

        /// <summary>
        /// Muestra la información del docente
        /// </summary>
        public string MostrarInformacion()
        {
            return $"Cédula: {Cedula}, Nombre: {Nombre} {Apellido}, Asignaturas: {contadorAsignaturas}";
        }
    }
}
