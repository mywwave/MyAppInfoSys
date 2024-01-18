using System;
using System.IO;
using System.Reflection;

class App
{
    public void Run()
    {
        Console.WriteLine("Пытаюсь собрать твои данные...");
    }
}

public class Program
{
    public static void Main()
    {
        App mainApp = new App();
        mainApp.Run();

        string dllFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "dll");
        if (!Directory.Exists(dllFolderPath))
        {
            Console.WriteLine("DLL папка не найдена.");
            return;
        }

        string[] dllFiles = Directory.GetFiles(dllFolderPath, "*.dll");
        if (dllFiles.Length == 0)
        {
            Console.WriteLine("Папка DLL пуста.");
            return;
        }

        foreach (string dllFile in dllFiles)
        {
            ProcessDllFile(dllFile);
        }
    }

    private static void ProcessDllFile(string dllFile)
    {
        try
        {
            Assembly assembly = Assembly.LoadFrom(dllFile);
            if (assembly == null)
            {
                Console.WriteLine("Что-то не так...");
                return;
            }

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                ProcessType(type);
            }
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("У меня нет инструментов...");
        }
    }

    private static void ProcessType(Type type)
    {
        MethodInfo[] methods = type.GetMethods();
        foreach (MethodInfo method in methods)
        {
            if (method.Name == "PrintPCInfo")
            {
                InvokeMethod(type, method, "Не могу вызвать метод PrintPCInfo");
            }
            else if (method.Name == "GetExternalIpAddress")
            {
                InvokeMethod(type, method, "Не могу вызвать метод GetExternalIpAddress");
            }
            else if (IsSupportedMethod(method))
            {
                continue;
            }
            else
            {
                Console.WriteLine($"Не поддерживающий: {method.Name}");
            }
        }
    }

    private static bool IsSupportedMethod(MethodInfo method)
    {
        return method.DeclaringType == typeof(object);
    }

    private static void InvokeMethod(Type type, MethodInfo method, string errorMessage)
    {
        try
        {
            object instance = Activator.CreateInstance(type);
            method.Invoke(instance, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{errorMessage}: {ex.Message}");
        }
    }
}