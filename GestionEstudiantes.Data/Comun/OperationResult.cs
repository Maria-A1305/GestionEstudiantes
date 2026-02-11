namespace GestionEstudiantes.Datos.Comun
{
    /// <summary>
    /// Clase para encapsular el resultado de las operaciones del sistema
    /// </summary>
    public class OperationResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public dynamic? Data { get; set; }

        public OperationResult()
        {
            Message = string.Empty;
            Success = false;
            Data = null;
        }

        public OperationResult(bool success, string message, dynamic data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
