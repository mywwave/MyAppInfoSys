using System;
using System.IO;
using System.Reflection;

class App
{
    public void Run()
    {
        Console.WriteLine("Собрал твои данные...");
    }
}


public class Program
{
    public static void Main()
    {
        App mainApp = new App();
        mainApp.Run();

        string assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "Checker.dll");
        Assembly? assembly = Assembly.LoadFrom(assemblyPath);
        if (assembly != null)
        {
            Type? type = assembly.GetType("Checker.PcInfo");
            if (type != null)
            {
                object? instance = Activator.CreateInstance(type);
                if (instance != null)
                {
                    MethodInfo? method = type.GetMethod("PrintPCInfo");
                    method?.Invoke(instance, null);
                }
            }
        }
    }
}