using GestionEstudiantes.Datos.Comun;

namespace GestionEstudiantes.Datos.Entidades
{
    /// <summary>
    /// Clase que representa una asignatura que puede tener varios grupos
    /// </summary>
    public class Asignatura
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        private Grupo[] grupos;
        private int contadorGrupos;
        private const int MAX_GRUPOS = 10;

        public Asignatura(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
            grupos = new Grupo[MAX_GRUPOS];
            contadorGrupos = 0;
        }

        /// <summary>
        /// Agrega un grupo a la asignatura
        /// </summary>
        public OperationResult AgregarGrupo(Grupo grupo)
        {
            try
            {
                if (grupo == null)
                {
                    return new OperationResult(false, "El grupo no puede ser nulo.");
                }

                if (contadorGrupos >= MAX_GRUPOS)
                {
                    return new OperationResult(false, "La asignatura ha alcanzado el máximo de grupos permitidos.");
                }

                // Verificar si el nombre del grupo ya existe
                for (int i = 0; i < contadorGrupos; i++)
                {
                    if (grupos[i].Nombre.Equals(grupo.Nombre, StringComparison.OrdinalIgnoreCase))
                    {
                        return new OperationResult(false, "Ya existe un grupo con ese nombre en la asignatura.");
                    }
                }

                grupos[contadorGrupos] = grupo;
                contadorGrupos++;
                return new OperationResult(true, "Grupo agregado exitosamente a la asignatura.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al agregar grupo: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca un grupo por nombre
        /// </summary>
        public OperationResult BuscarGrupo(string nombreGrupo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreGrupo))
                {
                    return new OperationResult(false, "El nombre del grupo no puede estar vacío.");
                }

                for (int i = 0; i < contadorGrupos; i++)
                {
                    if (grupos[i].Nombre.Equals(nombreGrupo, StringComparison.OrdinalIgnoreCase))
                    {
                        return new OperationResult(true, "Grupo encontrado.", grupos[i]);
                    }
                }

                return new OperationResult(false, "Grupo no encontrado.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al buscar grupo: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el listado de todos los grupos de la asignatura
        /// </summary>
        public string ObtenerListadoGrupos()
        {
            if (contadorGrupos == 0)
            {
                return "No hay grupos registrados en esta asignatura.";
            }

            string listado = $"\nGrupos de la asignatura '{Nombre}':\n";
            for (int i = 0; i < contadorGrupos; i++)
            {
                listado += $"{i + 1}. {grupos[i].Nombre} - {grupos[i].ObtenerNumeroEstudiantes()} estudiante(s)\n";
            }

            return listado;
        }

        /// <summary>
        /// Obtiene el número de grupos en la asignatura
        /// </summary>
        public int ObtenerNumeroGrupos()
        {
            return contadorGrupos;
        }

        /// <summary>
        /// Obtiene información de la asignatura
        /// </summary>
        public string MostrarInformacion()
        {
            return $"Código: {Codigo}, Nombre: {Nombre}, Grupos: {contadorGrupos}";
        }
    }
}
