namespace TeacherComputerRetrieval.Core.Exceptions
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException() : base("NO SUCH ROUTE") { }
    }
}