﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace PermissionAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirPath = Path.GetFullPath("../../../ExampleProjectApp/bin/Debug");
            Run(dirPath);
            Console.ReadKey();
        }

        private static void Run(string dirPath)
        {
            var pathAndMethodCalls = new List<PathAndMethodCalls>();

            foreach (var path in GetFiles(dirPath))
            {
                Console.WriteLine(path);
                var fullMethodNames = GetUniqueFullMethodNamesFromPath(path);

                var typeAndMethods = GetTypeAndMethods(fullMethodNames);

                pathAndMethodCalls.Add(new PathAndMethodCalls(path, typeAndMethods));
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
                    TypeAndMethodParser.TypeAndMethodParseResult.ParseResult.Success)
                {
                    typeAndMethods.Add(typeAndMethodResult.Result);
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
            var callCodes = new[]
            {
                //OpCodes.Call
                //,
                OpCodes.Callvirt
            };

            var fullMethodNames = new HashSet<string>();
            var module = ModuleDefinition.ReadModule(path);
            foreach (TypeDefinition type in module.Types)
            {
                foreach (var methodDefinition in type.Methods)
                {
                    if (methodDefinition.Body != null)
                    {
                        foreach (var instruction in methodDefinition.Body.Instructions)
                        {
                            if (callCodes.Contains(instruction.OpCode))
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
            return fullMethodNames;
        }
    }

    public class PathAndMethodCalls
    {
        public PathAndMethodCalls(string path, List<TypeAndMethod> calls)
        {
            Path = path;
            Calls = calls;
        }

        public string Path { get; }
        public List<TypeAndMethod> Calls { get; }
    }
}