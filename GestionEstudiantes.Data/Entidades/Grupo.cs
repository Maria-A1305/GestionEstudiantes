using GestionEstudiantes.Datos.Comun;

namespace GestionEstudiantes.Datos.Entidades
{
    /// <summary>
    /// Clase que representa un grupo de estudiantes
    /// </summary>
    public class Grupo
    {
        public string Nombre { get; set; }
        private Estudiante[] estudiantes;
        private int contadorEstudiantes;
        private const int MAX_ESTUDIANTES = 50;

        public Grupo(string nombre)
        {
            Nombre = nombre;
            estudiantes = new Estudiante[MAX_ESTUDIANTES];
            contadorEstudiantes = 0;
        }

        /// <summary>
        /// Agrega un estudiante al grupo
        /// </summary>
        public OperationResult AgregarEstudiante(Estudiante estudiante)
        {
            try
            {
                if (estudiante == null)
                {
                    return new OperationResult(false, "El estudiante no puede ser nulo.");
                }

                if (contadorEstudiantes >= MAX_ESTUDIANTES)
                {
                    return new OperationResult(false, "El grupo ha alcanzado el máximo de estudiantes permitidos.");
                }

                // Verificar si la matrícula ya existe en el grupo
                for (int i = 0; i < contadorEstudiantes; i++)
                {
                    if (estudiantes[i].Matricula == estudiante.Matricula)
                    {
                        return new OperationResult(false, "Ya existe un estudiante con esa matrícula en el grupo.");
                    }
                }

                estudiantes[contadorEstudiantes] = estudiante;
                contadorEstudiantes++;
                return new OperationResult(true, "Estudiante agregado exitosamente al grupo.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al agregar estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca un estudiante por matrícula
        /// </summary>
        public OperationResult BuscarEstudiante(string matricula)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(matricula))
                {
                    return new OperationResult(false, "La matrícula no puede estar vacía.");
                }

                for (int i = 0; i < contadorEstudiantes; i++)
                {
                    if (estudiantes[i].Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase))
                    {
                        return new OperationResult(true, "Estudiante encontrado.", estudiantes[i]);
                    }
                }

                return new OperationResult(false, "Estudiante no encontrado.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al buscar estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el listado de calificaciones de todos los estudiantes
        /// </summary>
        public string ObtenerListadoCalificaciones()
        {
            if (contadorEstudiantes == 0)
            {
                return "No hay estudiantes registrados en este grupo.";
            }

            string listado = $"\n{'═',80:D80}\n";
            listado += $"LISTADO DE CALIFICACIONES - GRUPO: {Nombre}\n";
            listado += $"{'═',80:D80}\n\n";

            for (int i = 0; i < contadorEstudiantes; i++)
            {
                Estudiante est = estudiantes[i];
                listado += $"{i + 1}. {est.MostrarInformacion()}\n";
                listado += $"   Promedio Exámenes: {est.CalcularPromedioExamenes():F2}\n";
                listado += $"   Promedio Prácticas: {est.CalcularPromedioPracticas():F2}\n";
                listado += $"   Nota Final: {est.CalcularNotaFinal():F2}\n";
                listado += $"   Estado: {(est.EstaAprobado() ? "APROBADO" : "REPROBADO")}\n";
                listado += $"{'-',80:D80}\n";
            }

            return listado;
        }

        /// <summary>
        /// Calcula el porcentaje de estudiantes aprobados en el grupo
        /// </summary>
        public double CalcularPorcentajeAprobados()
        {
            if (contadorEstudiantes == 0)
            {
                return 0;
            }

            int aprobados = 0;
            for (int i = 0; i < contadorEstudiantes; i++)
            {
                if (estudiantes[i].EstaAprobado())
                {
                    aprobados++;
                }
            }

            return (aprobados * 100.0) / contadorEstudiantes;
        }

        /// <summary>
        /// Obtiene el número de estudiantes en el grupo
        /// </summary>
        public int ObtenerNumeroEstudiantes()
        {
            return contadorEstudiantes;
        }

        /// <summary>
        /// Obtiene el reporte de aprobados/reprobados
        /// </summary>
        public string ObtenerReporteAprobacion()
        {
            if (contadorEstudiantes == 0)
            {
                return "No hay estudiantes registrados en este grupo.";
            }

            int aprobados = 0;
            int reprobados = 0;

            for (int i = 0; i < contadorEstudiantes; i++)
            {
                if (estudiantes[i].EstaAprobado())
                {
                    aprobados++;
                }
                else
                {
                    reprobados++;
                }
            }

            string reporte = $"\n{'═',60:D60}\n";
            reporte += $"REPORTE DE APROBACIÓN - GRUPO: {Nombre}\n";
            reporte += $"{'═',60:D60}\n";
            reporte += $"Total de estudiantes: {contadorEstudiantes}\n";
            reporte += $"Aprobados: {aprobados}\n";
            reporte += $"Reprobados: {reprobados}\n";
            reporte += $"Porcentaje de aprobación: {CalcularPorcentajeAprobados():F2}%\n";
            reporte += $"{'═',60:D60}\n";

            return reporte;
        }
    }
}
