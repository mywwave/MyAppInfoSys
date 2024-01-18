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

        string dllFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "dll");
        string[] dllFiles = Directory.GetFiles(dllFolderPath, "*.dll");
        foreach (string dllFile in dllFiles)
        {
            Assembly? assembly = Assembly.LoadFrom(dllFile);
            if (assembly != null)
            {
                Type?[] types = assembly.GetTypes();
                foreach (Type? type in types)
                {
                    MethodInfo?[] methods = type.GetMethods();
                    foreach (MethodInfo? method in methods)
                    {
                        if (method.Name == "PrintPCInfo")
                        {
                            object? instance = Activator.CreateInstance(type);
                            method.Invoke(instance, null);
                        }
                        else if (method.Name == "GetExternalIpAddress")
                        {
                            object? instance = Activator.CreateInstance(type);
                            method.Invoke(instance, null);
                        }
                    }
                }
            }
        }
    }
}