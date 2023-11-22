namespace RedBrowTest.Core.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string objectName, object key) : base($"El objeto \"{objectName}\" con clave ({key}) no fue encontrado o no existe.")
        {
        }

        public NotFoundException(string objectName) : base($"El objeto \"{objectName}\" no fue encontrado o no existe.")
        {
        }
    }
}
