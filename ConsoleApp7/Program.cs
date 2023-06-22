using System;
using System.Linq;
using System.Reflection;
using System.Text;
using dnlib.DotNet;
using System.IO;
using dnlib.DotNet.Emit;
using System.Collections.Generic;
using dnlib.DotNet.Writer;
using ConsoleApp7.ProxyMethods;

namespace ConsoleApp7
{
    internal class Program
    {
        private static ModuleDefMD module;

        static void Main(string[] args)
        {
            module = ModuleDefMD.Load(args[0]);

            //Math.CleanBasicMathMutation(module);
            /*
                        AntiAntiDump(module);
                        AntiAntiVM(module */
            ProxyFixer.RemoveProxyCalls(module);
            ProxyFixer.RemoveJunksMethods(module);
          /*  Math.CleanMath(module);
            Math.CleanMath2(module);
            Math.CleanMath3(module);
            Math.CleanMath4(module);
            Math.CleanFirstGradeMath(module);
            Math.CleanSecondGradeMath(module);
                                                        //   Math.CleanBasicMathMutation(module);
            Math.FinishMath();
            StringDecryption(module);
            StringDecryptionSecondPass(module);
            StringDecryptionSecondPassWithMathEncryption(module);
            StringDecryptionSecondPassWithMathMutation(module);*/

            //   StringDecryptionSecondPass(module);

            // Easier to do manually 


            var outputPath = Path.GetFileNameWithoutExtension(args[0]) + "_deobfed.exe";
            module.Write(outputPath, new ModuleWriterOptions(module)
            {
                Logger = DummyLogger.NoThrowInstance
            });
            Console.ReadKey();

        }

        

       /* public static void NopMethodsInMain(ModuleDefMD module, int targetMethodLength)
        {
            var programType = module.Types.FirstOrDefault(t => t.Name == "Program");
            if (programType == null)
                return;

            var mainMethod = programType.Methods.FirstOrDefault(m => m.Name == "Main");
            if (mainMethod == null)
                return;

            foreach (var instruction in mainMethod.Body.Instructions)
            {
                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodDef calledMethod)
                {
                    if (calledMethod.Body.Instructions.Count == targetMethodLength)
                    {
                        // Nop the instructions in the called method
                        foreach (var calledInstruction in calledMethod.Body.Instructions)
                        {
                            calledInstruction.OpCode = OpCodes.Nop;
                            calledInstruction.Operand = null;
                        }
                    }
                    else
                    {
                        // Set the call instruction to nop/null
                        instruction.OpCode = OpCodes.Nop;
                        instruction.Operand = null;

                        // Stop the loop since we've found a method with a different length
                        break;
                    }
                }
            }
        }*/

        public static void AntiAntiDump(ModuleDefMD module)
        {
            MethodDef virtualProtectMethod = null;

            // Find the method containing the VirtualProtect call
            foreach (var typeDef in module.GetTypes())
            {
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody)
                        continue;

                    var instructions = methodDef.Body.Instructions;

                    for (int i = 0; i < instructions.Count - 1; i++)
                    {
                        if (instructions[i].OpCode == OpCodes.Call &&
                            instructions[i].Operand is MethodDef calledMethod &&
                            calledMethod.Name == "VirtualProtect")
                        {
                            virtualProtectMethod = methodDef;
                            break;
                        }
                    }

                    if (virtualProtectMethod != null)
                        break;
                }

                if (virtualProtectMethod != null)
                    break;
            }

            if (virtualProtectMethod == null)
                return;

            // Nop the instructions in the method containing the VirtualProtect call
            foreach (var instruction in virtualProtectMethod.Body.Instructions)
            {
                instruction.OpCode = OpCodes.Nop;
                instruction.Operand = null;
            }

            // Nop the calls to the method
            foreach (var typeDef in module.GetTypes())
            {
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody)
                        continue;

                    var instructions = methodDef.Body.Instructions;

                    for (int i = 0; i < instructions.Count; i++)
                    {
                        if (instructions[i].OpCode == OpCodes.Call && instructions[i].Operand == virtualProtectMethod)
                        {
                            instructions[i].OpCode = OpCodes.Nop;
                            instructions[i].Operand = null;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Typewrite("\n\n       # Removed AntiDump #");
        }

/*        public static void AntiAntiDump(ModuleDefMD module)
        {
            foreach (var typeDef in module.GetTypes())
            {
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody)
                        continue;

                    var instructions = methodDef.Body.Instructions;

                    for (int i = 0; i < instructions.Count; i++)
                    {
                        if (instructions[i].OpCode == OpCodes.Call && instructions[i].Operand is MethodDef calledMethod && calledMethod.Name == "VirtualProtect")
                        {
                            // Perform the nop operation on the instructions
                            for (int j = i; j < i + 9; j++)
                            {
                                instructions[j].OpCode = OpCodes.Nop;
                                instructions[j].Operand = null;
                            }

                            // Adjust the instruction index accordingly
                            i += 8;
                        }
                    }
                }
            }
        }*/

        
        public static void AntiAntiVM(ModuleDefMD module2)
        {
            int callsRemoved = 0; // Counter variable

            foreach (var typeDef in module2.GetTypes())
            {
                foreach (var methodDef in typeDef.Methods)
                {
                    if (methodDef.Name == "Main" && methodDef.HasBody)
                    {
                        var instructions = methodDef.Body.Instructions;

                        for (var i = 0; i < instructions.Count; i++)
                        {
                            if (instructions[i].OpCode.Code == Code.Call)
                            {
                                instructions.RemoveAt(i);
                                i--; // Decrement i to adjust for the removed instruction

                                callsRemoved++; // Increment the counter

                                if (callsRemoved >= 2) // Check if the limit is reached
                                    break;
                            }
                        }
                    }

                    if (callsRemoved >= 2) // Check if the limit is reached
                        break;
                }

                if (callsRemoved >= 2) // Check if the limit is reached
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Typewrite("\n\n       # Removed AntiVM #");
        }

        public static string DecryptResourceFromAssembly(ModuleDefMD assemblyPath, string resourceName)
        {
            string result;
            Assembly targetAssembly = Assembly.LoadFile(assemblyPath.Location);
            using (Stream manifestResourceStream = targetAssembly.GetManifestResourceStream(resourceName))
            {
                if (manifestResourceStream == null)
                {
                    result = null;
                }
                else
                {
                    byte[] array = new byte[manifestResourceStream.Length];
                    manifestResourceStream.Read(array, 0, array.Length);
                    result = Encoding.UTF8.GetString(array);
                }
            }
            return result;
        }

        public static string secondecrypt(string A_0, int A_1, object A_2)
        {
            StringBuilder stringBuilder = new StringBuilder(A_0.Length);
            int num2 = 0;
            StringBuilder stringBuilder2 = new StringBuilder(A_0);
            char c;

            while (num2 < A_0.Length)
            {
                c = stringBuilder2[num2];
                c = (char)((int)c ^ A_1);
                stringBuilder.Append(c);
                num2++;
            }

            return stringBuilder.ToString();
        }

        public static void StringDecryptionSecondPassWithMathEncryption(ModuleDefMD module2)
        {
            int decryptedStringCount = 0;

            foreach (var typeDef in module2.GetTypes())
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody) continue;
                    var instructions = new List<Instruction>(methodDef.Body.Instructions);

                    for (var i = instructions.Count - 1; i >= 0; i--)
                        if (instructions[i].OpCode.Code == Code.Ldstr &&
                            instructions[i + 3].OpCode.Code == Code.Ldc_I4 &&
                            instructions[i + 4].OpCode.Code == Code.Ldnull &&
                            instructions[i + 5].OpCode.Code == Code.Call)
                        {
                            var key = (int)instructions[i + 3].Operand;
                            var String = (string)instructions[i].Operand;
                            var Object = (object)instructions[i + 4].Operand;
                            string DecryptedString = secondecrypt(String, key, Object);
                            instructions[i].Operand = DecryptedString;
                            instructions[i + 5].OpCode = OpCodes.Nop;
                            instructions[i + 5].Operand = null;
                            instructions[i + 3].OpCode = OpCodes.Nop;
                            instructions[i + 3].Operand = null;
                            instructions[i + 4].OpCode = OpCodes.Nop;
                            instructions[i + 4].Operand = null;


                            decryptedStringCount++;
                        }
                }
            /*            Console.ForegroundColor = ConsoleColor.Red;
                        Typewrite("\n\n# Finished Strings: " + decryptedStringCount + " #");*/

            Console.ForegroundColor = ConsoleColor.Red;
            if (decryptedStringCount != 0)
                Typewrite($"\n\n       # Finished  {decryptedStringCount} Strings w/ Math Ops #");
        }

        public static void StringDecryptionSecondPass(ModuleDefMD module2)
        {
            int decryptedStringCount = 0;

            foreach (var typeDef in module2.GetTypes())
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody) continue;
                    var instructions = new List<Instruction>(methodDef.Body.Instructions);

                    for (var i = instructions.Count - 1; i >= 0; i--)
                        if (instructions[i].OpCode.Code == Code.Ldstr &&
                            instructions[i + 4].OpCode.Code == Code.Call &&
                            instructions[i + 2].OpCode.Code == Code.Ldc_I4 &&
                            instructions[i + 3].OpCode.Code == Code.Ldnull)
                        {
                            var key = (int)instructions[i + 2].Operand;
                            var String = (string)instructions[i].Operand;
                            var Object = (object)instructions[i + 3].Operand;
                            string DecryptedString = secondecrypt(String, key, Object);
                            instructions[i].Operand = DecryptedString;
                            instructions[i + 1].OpCode = OpCodes.Nop;
                            instructions[i + 1].Operand = null;
                            instructions[i + 2].OpCode = OpCodes.Nop;
                            instructions[i + 2].Operand = null;
                            instructions[i + 3].OpCode = OpCodes.Nop;
                            instructions[i + 3].Operand = null;
                            instructions[i + 4].OpCode = OpCodes.Nop;
                            instructions[i + 4].Operand = null;


                            decryptedStringCount++;
                        }
                }
 /*           Console.ForegroundColor = ConsoleColor.Red;
            Typewrite("\n\n# Finished Strings: " + decryptedStringCount + " #");*/

            Console.ForegroundColor = ConsoleColor.Red;
            if (decryptedStringCount != 0)
                Typewrite($"\n\n       # Finished  {decryptedStringCount} Strings #");
        }

        public static void StringDecryptionSecondPassWithMathMutation(ModuleDefMD module2)
        {
            int decryptedStringCount = 0;

            foreach (var typeDef in module2.GetTypes())
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody) continue;
                    var instructions = new List<Instruction>(methodDef.Body.Instructions);

                    for (var i = instructions.Count - 1; i >= 0; i--)
                        if (instructions[i].OpCode.Code == Code.Ldstr &&
                            instructions[i + 4].OpCode.Code == Code.Call &&
                            instructions[i + 2].OpCode.Code == Code.Ldc_I4_S &&
                            instructions[i + 3].OpCode.Code == Code.Ldnull)
                        {
                            var key = (int)instructions[i + 2].GetLdcI4Value();
                            var String = (string)instructions[i].Operand;
                            var Object = (object)instructions[i + 3].Operand;
                            string DecryptedString = secondecrypt(String, key, Object);
                            instructions[i].Operand = DecryptedString;
                            instructions[i + 1].OpCode = OpCodes.Nop;
                            instructions[i + 1].Operand = null;
                            instructions[i + 2].OpCode = OpCodes.Nop;
                            instructions[i + 2].Operand = null;
                            instructions[i + 3].OpCode = OpCodes.Nop;
                            instructions[i + 3].Operand = null;
                            instructions[i + 4].OpCode = OpCodes.Nop;
                            instructions[i + 4].Operand = null;


                            decryptedStringCount++;
                        }
                }
            /*           Console.ForegroundColor = ConsoleColor.Red;
                       Typewrite("\n\n# Finished Strings: " + decryptedStringCount + " #");*/

            Console.ForegroundColor = ConsoleColor.Red;
            if (decryptedStringCount != 0)
                Typewrite($"\n\n       # Finished  {decryptedStringCount} w/ Math Mutation #");
        }

        public static void StringDecryption(ModuleDefMD module2)
        {
            int decryptedStringCount = 0;

            foreach (var typeDef in module2.GetTypes())
                foreach (var methodDef in typeDef.Methods)
                {
                    if (!methodDef.HasBody) continue;
                    var instructions = new List<Instruction>(methodDef.Body.Instructions);

                    for (var i = instructions.Count - 1; i >= 0; i--)
                        if (instructions[i].OpCode.Code == Code.Ldstr &&
                            instructions[i + 1].OpCode.Code == Code.Call)
                        {
                          //  var key = (int)instructions[i + 2].Operand;
                            var String = (string)instructions[i].Operand;
                            string DecryptedString = DecryptResourceFromAssembly(module2, String);
                            instructions[i].Operand = DecryptedString;
                            instructions[i + 1].OpCode = OpCodes.Nop; 
                            instructions[i + 1].Operand = null; 

                            decryptedStringCount++;
                        }
                }
/*            Console.ForegroundColor = ConsoleColor.Red;
            Typewrite("\n\n# Decrypted Strings: " + decryptedStringCount + " #");*/

            Console.ForegroundColor = ConsoleColor.Red;
            Typewrite($"\n\n       # Decrypted  {decryptedStringCount} Strings #");
        }

        /*    public static string Yosef(string A_0, int A_1)
            {

                A_0 = A_0.ToString().Replace("                                                                                                                                       ", "");
                StringBuilder stringBuilder = new StringBuilder(A_0);
                StringBuilder stringBuilder2 = new StringBuilder(A_0.Length);
                for (int i = 0; i < A_0.Length; i++)
                {
                    char c = stringBuilder[i];
                    c = (char)((int)c ^ A_1);
                    stringBuilder2.Append(c);
                }
                return stringBuilder2.ToString();
            }*/

        static void Typewrite(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(1);
            }

        }
    }
}
