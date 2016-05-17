namespace OrcaML.Tests.Integration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new[] { "BasicFill" };
            }
            switch (args[0]) {
            case "BasicFill":
                BasicFill.Run();
                break;
            case "BasicSquare":
                BasicSquare.Run();
                break;
            }
        }
    }
}
