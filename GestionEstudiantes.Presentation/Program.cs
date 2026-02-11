using GestionEstudiantes.Datos.Comun;
using GestionEstudiantes.Datos.Entidades;
using GestionEstudiantes.Presentacion.Utilidades;

namespace GestionEstudiantes.Presentacion
{
    /// <summary>
    /// Clase principal del programa que gestiona el menú y las operaciones del sistema
    /// </summary>
    class Program
    {
        private static Docente docenteActual = null!;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            InicializarSistema();
            MostrarMenuPrincipal();
        }

        /// <summary>
        /// Inicializa el sistema solicitando datos del docente
        /// </summary>
        static void InicializarSistema()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Sistema de Gestión de Estudiantes");

            Console.WriteLine("\nBienvenido al Sistema de Gestión de Estudiantes");
            Console.WriteLine("Por favor, ingrese sus datos:\n");

            string cedula = UtilidadesConsola.LeerTextoNoVacio("Cédula: ");
            string nombre = UtilidadesConsola.LeerTextoNoVacio("Nombre: ");
            string apellido = UtilidadesConsola.LeerTextoNoVacio("Apellido: ");

            docenteActual = new Docente(cedula, nombre, apellido);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Docente registrado exitosamente: {nombre} {apellido}");
            Console.ResetColor();

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Muestra el menú principal del sistema
        /// </summary>
        static void MostrarMenuPrincipal()
        {
            int opcion;

            do
            {
                UtilidadesConsola.LimpiarConsola();
                UtilidadesConsola.MostrarEncabezado($"Docente: {docenteActual.Nombre} {docenteActual.Apellido}");

                Console.WriteLine("\n MENÚ PRINCIPAL");
                Console.WriteLine(new string('─', 80));
                Console.WriteLine(" 1. Gestionar Asignaturas");
                Console.WriteLine(" 2. Gestionar Grupos");
                Console.WriteLine(" 3. Gestionar Estudiantes");
                Console.WriteLine(" 4. Registrar Calificaciones");
                Console.WriteLine(" 5. Reportes y Consultas");
                Console.WriteLine(" 0. Salir");
                Console.WriteLine(new string('─', 80));

                opcion = UtilidadesConsola.LeerEntero("\nSeleccione una opción: ", 0, 5);

                switch (opcion)
                {
                    case 1:
                        MenuAsignaturas();
                        break;
                    case 2:
                        MenuGrupos();
                        break;
                    case 3:
                        MenuEstudiantes();
                        break;
                    case 4:
                        MenuCalificaciones();
                        break;
                    case 5:
                        MenuReportes();
                        break;
                    case 0:
                        Console.WriteLine("\n¡Gracias por usar el sistema! Hasta pronto.");
                        break;
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Menú para gestionar asignaturas
        /// </summary>
        static void MenuAsignaturas()
        {
            int opcion;

            do
            {
                UtilidadesConsola.LimpiarConsola();
                UtilidadesConsola.MostrarEncabezado("Gestión de Asignaturas");

                Console.WriteLine("\n 1. Agregar nueva asignatura");
                Console.WriteLine(" 2. Listar asignaturas");
                Console.WriteLine(" 0. Volver al menú principal");
                Console.WriteLine(new string('─', 80));

                opcion = UtilidadesConsola.LeerEntero("\nSeleccione una opción: ", 0, 2);

                switch (opcion)
                {
                    case 1:
                        AgregarAsignatura();
                        break;
                    case 2:
                        ListarAsignaturas();
                        break;
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Agrega una nueva asignatura
        /// </summary>
        static void AgregarAsignatura()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Agregar Nueva Asignatura");

            try
            {
                string codigo = UtilidadesConsola.LeerTextoNoVacio("\nCódigo de la asignatura: ");
                string nombre = UtilidadesConsola.LeerTextoNoVacio("Nombre de la asignatura: ");

                Asignatura nuevaAsignatura = new Asignatura(codigo, nombre);
                OperationResult resultado = docenteActual.AgregarAsignatura(nuevaAsignatura);

                Console.WriteLine();
                UtilidadesConsola.MostrarResultadoOperacion(resultado);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Lista todas las asignaturas del docente
        /// </summary>
        static void ListarAsignaturas()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Listado de Asignaturas");

            Console.WriteLine(docenteActual.ObtenerListadoAsignaturas());

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Menú para gestionar grupos
        /// </summary>
        static void MenuGrupos()
        {
            int opcion;

            do
            {
                UtilidadesConsola.LimpiarConsola();
                UtilidadesConsola.MostrarEncabezado("Gestión de Grupos");

                Console.WriteLine("\n 1. Agregar nuevo grupo a una asignatura");
                Console.WriteLine(" 2. Listar grupos de una asignatura");
                Console.WriteLine(" 0. Volver al menú principal");
                Console.WriteLine(new string('─', 80));

                opcion = UtilidadesConsola.LeerEntero("\nSeleccione una opción: ", 0, 2);

                switch (opcion)
                {
                    case 1:
                        AgregarGrupo();
                        break;
                    case 2:
                        ListarGrupos();
                        break;
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Agrega un nuevo grupo a una asignatura
        /// </summary>
        static void AgregarGrupo()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Agregar Nuevo Grupo");

            try
            {
                if (docenteActual.ObtenerNumeroAsignaturas() == 0)
                {
                    Console.WriteLine("\nNo hay asignaturas registradas. Por favor, agregue una asignatura primero.");
                    UtilidadesConsola.PausarConsola();
                    return;
                }

                Console.WriteLine(docenteActual.ObtenerListadoAsignaturas());

                string codigoAsignatura = UtilidadesConsola.LeerTextoNoVacio("\nIngrese el código de la asignatura: ");
                OperationResult resultadoBusqueda = docenteActual.BuscarAsignatura(codigoAsignatura);

                if (!resultadoBusqueda.Success)
                {
                    Console.WriteLine();
                    UtilidadesConsola.MostrarResultadoOperacion(resultadoBusqueda);
                    UtilidadesConsola.PausarConsola();
                    return;
                }

                Asignatura asignatura = (Asignatura)resultadoBusqueda.Data;
                string nombreGrupo = UtilidadesConsola.LeerTextoNoVacio("Nombre del grupo: ");

                Grupo nuevoGrupo = new Grupo(nombreGrupo);
                OperationResult resultado = asignatura.AgregarGrupo(nuevoGrupo);

                Console.WriteLine();
                UtilidadesConsola.MostrarResultadoOperacion(resultado);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Lista los grupos de una asignatura
        /// </summary>
        static void ListarGrupos()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Listado de Grupos");

            try
            {
                if (docenteActual.ObtenerNumeroAsignaturas() == 0)
                {
                    Console.WriteLine("\nNo hay asignaturas registradas.");
                    UtilidadesConsola.PausarConsola();
                    return;
                }

                Console.WriteLine(docenteActual.ObtenerListadoAsignaturas());

                string codigoAsignatura = UtilidadesConsola.LeerTextoNoVacio("\nIngrese el código de la asignatura: ");
                OperationResult resultado = docenteActual.BuscarAsignatura(codigoAsignatura);

                if (!resultado.Success)
                {
                    Console.WriteLine();
                    UtilidadesConsola.MostrarResultadoOperacion(resultado);
                }
                else
                {
                    Asignatura asignatura = (Asignatura)resultado.Data;
                    Console.WriteLine(asignatura.ObtenerListadoGrupos());
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Menú para gestionar estudiantes
        /// </summary>
        static void MenuEstudiantes()
        {
            int opcion;

            do
            {
                UtilidadesConsola.LimpiarConsola();
                UtilidadesConsola.MostrarEncabezado("Gestión de Estudiantes");

                Console.WriteLine("\n 1. Agregar estudiante presencial");
                Console.WriteLine(" 2. Agregar estudiante a distancia");
                Console.WriteLine(" 3. Listar estudiantes de un grupo");
                Console.WriteLine(" 0. Volver al menú principal");
                Console.WriteLine(new string('─', 80));

                opcion = UtilidadesConsola.LeerEntero("\nSeleccione una opción: ", 0, 3);

                switch (opcion)
                {
                    case 1:
                        AgregarEstudiante(true);
                        break;
                    case 2:
                        AgregarEstudiante(false);
                        break;
                    case 3:
                        ListarEstudiantes();
                        break;
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Agrega un estudiante a un grupo
        /// </summary>
        static void AgregarEstudiante(bool esPresencial)
        {
            UtilidadesConsola.LimpiarConsola();
            string tipoEstudiante = esPresencial ? "Presencial" : "A Distancia";
            UtilidadesConsola.MostrarEncabezado($"Agregar Estudiante {tipoEstudiante}");

            try
            {
                Grupo grupo = SeleccionarGrupo();
                if (grupo == null) return;

                Console.WriteLine($"\nIngrese los datos del estudiante {tipoEstudiante.ToLower()}:\n");

                string matricula = UtilidadesConsola.LeerTextoNoVacio("Matrícula: ");
                string nombre = UtilidadesConsola.LeerTextoNoVacio("Nombre: ");
                string apellido = UtilidadesConsola.LeerTextoNoVacio("Apellido: ");

                Estudiante nuevoEstudiante;
                if (esPresencial)
                {
                    nuevoEstudiante = new EstudiantePresencial(matricula, nombre, apellido);
                }
                else
                {
                    nuevoEstudiante = new EstudianteDistancia(matricula, nombre, apellido);
                }

                OperationResult resultado = grupo.AgregarEstudiante(nuevoEstudiante);

                Console.WriteLine();
                UtilidadesConsola.MostrarResultadoOperacion(resultado);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Lista los estudiantes de un grupo
        /// </summary>
        static void ListarEstudiantes()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Listado de Estudiantes");

            try
            {
                Grupo grupo = SeleccionarGrupo();
                if (grupo == null) return;

                Console.WriteLine($"\n{'═',80:D80}");
                Console.WriteLine($"ESTUDIANTES DEL GRUPO: {grupo.Nombre}");
                Console.WriteLine($"{'═',80:D80}");

                if (grupo.ObtenerNumeroEstudiantes() == 0)
                {
                    Console.WriteLine("\nNo hay estudiantes registrados en este grupo.");
                }
                else
                {
                    Console.WriteLine($"\nTotal de estudiantes: {grupo.ObtenerNumeroEstudiantes()}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Menú para registrar calificaciones
        /// </summary>
        static void MenuCalificaciones()
        {
            int opcion;

            do
            {
                UtilidadesConsola.LimpiarConsola();
                UtilidadesConsola.MostrarEncabezado("Registro de Calificaciones");

                Console.WriteLine("\n 1. Registrar calificación de examen");
                Console.WriteLine(" 2. Registrar calificación de práctica");
                Console.WriteLine(" 0. Volver al menú principal");
                Console.WriteLine(new string('─', 80));

                opcion = UtilidadesConsola.LeerEntero("\nSeleccione una opción: ", 0, 2);

                switch (opcion)
                {
                    case 1:
                        RegistrarCalificacion(true);
                        break;
                    case 2:
                        RegistrarCalificacion(false);
                        break;
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Registra una calificación para un estudiante
        /// </summary>
        static void RegistrarCalificacion(bool esExamen)
        {
            UtilidadesConsola.LimpiarConsola();
            string tipoCalificacion = esExamen ? "Examen" : "Práctica";
            UtilidadesConsola.MostrarEncabezado($"Registrar Calificación de {tipoCalificacion}");

            try
            {
                Grupo grupo = SeleccionarGrupo();
                if (grupo == null) return;

                if (grupo.ObtenerNumeroEstudiantes() == 0)
                {
                    Console.WriteLine("\nNo hay estudiantes registrados en este grupo.");
                    UtilidadesConsola.PausarConsola();
                    return;
                }

                string matricula = UtilidadesConsola.LeerTextoNoVacio("\nMatrícula del estudiante: ");
                OperationResult resultadoBusqueda = grupo.BuscarEstudiante(matricula);

                if (!resultadoBusqueda.Success)
                {
                    Console.WriteLine();
                    UtilidadesConsola.MostrarResultadoOperacion(resultadoBusqueda);
                    UtilidadesConsola.PausarConsola();
                    return;
                }

                Estudiante estudiante = (Estudiante)resultadoBusqueda.Data;
                Console.WriteLine($"\nEstudiante: {estudiante.MostrarInformacion()}");

                double calificacion = UtilidadesConsola.LeerDouble($"\nCalificación de {tipoCalificacion.ToLower()} (0-100): ", 0, 100);

                OperationResult resultado;
                if (esExamen)
                {
                    resultado = estudiante.AgregarCalificacionExamen(calificacion);
                }
                else
                {
                    resultado = estudiante.AgregarCalificacionPractica(calificacion);
                }

                Console.WriteLine();
                UtilidadesConsola.MostrarResultadoOperacion(resultado);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Menú de reportes y consultas
        /// </summary>
        static void MenuReportes()
        {
            int opcion;

            do
            {
                UtilidadesConsola.LimpiarConsola();
                UtilidadesConsola.MostrarEncabezado("Reportes y Consultas");

                Console.WriteLine("\n 1. Listado de calificaciones por grupo");
                Console.WriteLine(" 2. Reporte de aprobación por grupo");
                Console.WriteLine(" 0. Volver al menú principal");
                Console.WriteLine(new string('─', 80));

                opcion = UtilidadesConsola.LeerEntero("\nSeleccione una opción: ", 0, 2);

                switch (opcion)
                {
                    case 1:
                        MostrarListadoCalificaciones();
                        break;
                    case 2:
                        MostrarReporteAprobacion();
                        break;
                }

            } while (opcion != 0);
        }

        /// <summary>
        /// Muestra el listado de calificaciones de un grupo
        /// </summary>
        static void MostrarListadoCalificaciones()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Listado de Calificaciones");

            try
            {
                Grupo grupo = SeleccionarGrupo();
                if (grupo == null) return;

                Console.WriteLine(grupo.ObtenerListadoCalificaciones());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Muestra el reporte de aprobación de un grupo
        /// </summary>
        static void MostrarReporteAprobacion()
        {
            UtilidadesConsola.LimpiarConsola();
            UtilidadesConsola.MostrarEncabezado("Reporte de Aprobación");

            try
            {
                Grupo grupo = SeleccionarGrupo();
                if (grupo == null) return;

                Console.WriteLine(grupo.ObtenerReporteAprobacion());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            UtilidadesConsola.PausarConsola();
        }

        /// <summary>
        /// Método auxiliar para seleccionar un grupo
        /// </summary>
        static Grupo SeleccionarGrupo()
        {
            try
            {
                if (docenteActual.ObtenerNumeroAsignaturas() == 0)
                {
                    Console.WriteLine("\nNo hay asignaturas registradas. Por favor, agregue una asignatura primero.");
                    UtilidadesConsola.PausarConsola();
                    return null;
                }

                Console.WriteLine(docenteActual.ObtenerListadoAsignaturas());

                string codigoAsignatura = UtilidadesConsola.LeerTextoNoVacio("\nIngrese el código de la asignatura: ");
                OperationResult resultadoAsignatura = docenteActual.BuscarAsignatura(codigoAsignatura);

                if (!resultadoAsignatura.Success)
                {
                    Console.WriteLine();
                    UtilidadesConsola.MostrarResultadoOperacion(resultadoAsignatura);
                    UtilidadesConsola.PausarConsola();
                    return null;
                }

                Asignatura asignatura = (Asignatura)resultadoAsignatura.Data;

                if (asignatura.ObtenerNumeroGrupos() == 0)
                {
                    Console.WriteLine("\nNo hay grupos registrados en esta asignatura. Por favor, agregue un grupo primero.");
                    UtilidadesConsola.PausarConsola();
                    return null;
                }

                Console.WriteLine(asignatura.ObtenerListadoGrupos());

                string nombreGrupo = UtilidadesConsola.LeerTextoNoVacio("\nIngrese el nombre del grupo: ");
                OperationResult resultadoGrupo = asignatura.BuscarGrupo(nombreGrupo);

                if (!resultadoGrupo.Success)
                {
                    Console.WriteLine();
                    UtilidadesConsola.MostrarResultadoOperacion(resultadoGrupo);
                    UtilidadesConsola.PausarConsola();
                    return null;
                }

                return (Grupo)resultadoGrupo.Data;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error inesperado: {ex.Message}");
                Console.ResetColor();
                UtilidadesConsola.PausarConsola();
                return null;
            }
        }
    }
}