using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using PermissionAnalyser.Calls;

namespace PermissionAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirPath = Path.GetFullPath("../../../ExampleProjectApp/bin/Debug");
            Run(dirPath);
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        private static void Run(string dirPath)
        {
            var permissions = TypeConfig.GetPermissions();
            
            var pathAndActions = new List<PathAndActions>();

            foreach (var path in GetFiles(dirPath))
            {
                var actions = new List<string>();
                Console.WriteLine(path);
                var fullMethodNames = GetUniqueFullMethodNamesFromPath(path);

                var typeAndMethods = GetTypeAndMethods(fullMethodNames);
                foreach (var typeAndMethod in typeAndMethods)
                {
                    foreach (var permission in permissions)
                    {
                        if (permission.IsTypeAndMethod(typeAndMethod))
                        {
                            Console.WriteLine($"Adding action - {permission.Action}");
                            actions.Add(permission.Action);
                        }
                    }
                }
                pathAndActions.Add(new PathAndActions(path, actions));
            }
        }

        private static List<TypeAndMethod> GetTypeAndMethods(HashSet<string> fullMethodNames)
        {
            var parser = new TypeAndMethodParser();
            var typeAndMethods = new List<TypeAndMethod>();
            foreach (var fullMethodName in fullMethodNames)
            {
                var typeAndMethodResult = parser.Parse(fullMethodName);
                if (typeAndMethodResult.Outcome ==
                    TypeAndMethodParser.Result.ParseOutcome.Success)
                {
                    typeAndMethods.Add(typeAndMethodResult.Value);
                }
                else
                {
                    Console.WriteLine($"Cannot parse {fullMethodName}");
                }
            }
            return typeAndMethods;
        }

        private static IEnumerable<string> GetFiles(string dirPath)
        {
            Func<string, bool> fileIncluded = path =>
            {
                var fileName = Path.GetFileName(path);
                return fileName.StartsWith("AWSSDK.") == false;
            };
            var dllPaths = Directory.GetFiles(dirPath, "*.dll");
            var exePaths = Directory.GetFiles(dirPath, "*.exe");
            var paths = dllPaths.Concat(exePaths).Where(fileIncluded);
            return paths;
        }

        private static HashSet<string> GetUniqueFullMethodNamesFromPath(string path)
        {
            var fullMethodNames = new HashSet<string>();
            var module = ModuleDefinition.ReadModule(path);
            foreach (var type in module.Types)
            {
                CheckTypeRec(type, fullMethodNames);
            }
            return fullMethodNames;
        }

        private static void CheckTypeRec(TypeDefinition type, HashSet<string> fullMethodNames)
        {
            Console.WriteLine($"  Type - {type.Name}");
            GetTypeMethods(type, fullMethodNames);

            foreach (var typeDefinition in type.NestedTypes)
            {
                CheckTypeRec(typeDefinition, fullMethodNames);
            }
        }

        private static void GetTypeMethods(TypeDefinition type, HashSet<string> fullMethodNames)
        {
            foreach (var methodDefinition in type.Methods)
            {
                Console.WriteLine($"  Method - {methodDefinition.Name}");
                if (methodDefinition.Body != null)
                {
                    foreach (var instruction in methodDefinition.Body.Instructions)
                    {
                        Console.WriteLine(instruction.ToString());
                        if (instruction.OpCode == OpCodes.Callvirt)
                        {
                            var methodCall = instruction.Operand as MethodReference;
                            if (methodCall != null)
                            {
                                fullMethodNames.Add(methodCall.FullName);
                            }
                        }
                    }
                }
            }
        }
    }
}
