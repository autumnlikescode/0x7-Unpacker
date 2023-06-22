using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Data;
using System.Data.SqlTypes;
using System.Net.NetworkInformation;

namespace ConsoleApp7
{
    internal class Math
    {
        public static int mathcock;

        public static void FinishMath()
        {
            Typewrite($"\n\n       # Cleaned {Math.mathcock} Math Operations #");
        }


        public static void CleanMath(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4)
                        {
                            int one = method.Body.Instructions[i].GetLdcI4Value();
                            int two = method.Body.Instructions[i + 1].GetLdcI4Value();
                            int three = method.Body.Instructions[i + 3].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 2].OpCode.ToString());
                            string opTwo = islemDondur(method.Body.Instructions[i + 4].OpCode.ToString());

                            int value = (int)(Eval(one, opOne, two, opTwo, three));

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value;
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);

                            mathcock++;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            // Typewrite($"\n\n       # Cleaned  {mathops} Math Operations #");
        }

        public static void CleanMath3(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count - 3; i++)
                    {
                        if ((method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_S) &&
                            (method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_S) &&
                            (method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_S))
                        {
                            int one = method.Body.Instructions[i].GetLdcI4Value();
                            int two = method.Body.Instructions[i + 1].GetLdcI4Value();
                            int three = method.Body.Instructions[i + 3].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 2].OpCode.ToString());
                            string opTwo = islemDondur(method.Body.Instructions[i + 4].OpCode.ToString());

                            int value = (int)(Eval(one, opOne, two, opTwo, three));

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value;
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);

                            mathcock++;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            //  Typewrite($"\n\n       # Cleaned  {mathops} Math Operations #");
        }

        public static void CleanMath2(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count - 3; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_S &&
                            method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_S &&
                            method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_S)
                        {
                            int one = (sbyte)method.Body.Instructions[i].GetLdcI4Value();
                            int two = (sbyte)method.Body.Instructions[i + 1].GetLdcI4Value();
                            int three = (sbyte)method.Body.Instructions[i + 3].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 2].OpCode.ToString());
                            string opTwo = islemDondur(method.Body.Instructions[i + 4].OpCode.ToString());

                            int value = (int)(Eval(one, opOne, two, opTwo, three));

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value;
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);

                            mathcock++;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            //Typewrite($"\n\n       # Cleaned  {mathops} Math Operations #");
        }

        public static void CleanMath4(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count - 3; i++)
                    {
                        if ((method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_S ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_0 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_1 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_2 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_3 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_4 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_5 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_6 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_7 ||
                             method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4_8) &&
                            (method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_S ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_0 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_1 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_2 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_3 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_4 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_5 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_6 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_7 ||
                             method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4_8) &&
                            (method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_S ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_0 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_1 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_2 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_3 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_4 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_5 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_6 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_7 ||
                             method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4_8))
                        {
                            int one = method.Body.Instructions[i].GetLdcI4Value();
                            int two = method.Body.Instructions[i + 1].GetLdcI4Value();
                            int three = method.Body.Instructions[i + 3].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 2].OpCode.ToString());
                            string opTwo = islemDondur(method.Body.Instructions[i + 4].OpCode.ToString());

                            int value = (int)(Eval(one, opOne, two, opTwo, three));

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value;
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);

                            mathcock++;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
          //  Typewrite($"\n\n       # Cleaned  {mathops} Math Operations #");
        }


        public static void CleanBasicMathMutation(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count - 3; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 4].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 9].OpCode == OpCodes.Ldc_I4)
                        {
                            int one = (sbyte)method.Body.Instructions[i].GetLdcI4Value();
                            int two = (sbyte)method.Body.Instructions[i + 4].GetLdcI4Value();
                            int three = (sbyte)method.Body.Instructions[i + 9].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 5].OpCode.ToString());
                            string opTwo = islemDondur(method.Body.Instructions[i + 10].OpCode.ToString());

                            int value = (int)(Eval(one, opOne, two, opTwo, three));

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value;
                  /*          method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);*/

                            mathops++;
                        }
                    }
                }
            }
        }

        public static void CleanFirstGradeMath(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 1].OpCode == OpCodes.Ldc_I4)
                        {
                            int one = method.Body.Instructions[i].GetLdcI4Value();
                            int two = method.Body.Instructions[i + 1].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 3].OpCode.ToString());
                            int value = (int)EvalSimple(one, opOne, two);

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value;

                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            //          method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;

                            mathcock++;
                        }
                    }
                }
            }
        }

        public static void CleanSecondGradeMath(ModuleDefMD module)
        {
            int mathops = 0;
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 2].OpCode == OpCodes.Ldc_I4 &&
                            method.Body.Instructions[i + 4].OpCode == OpCodes.Ldc_I4)
                        {
                            int one = method.Body.Instructions[i].GetLdcI4Value();
                            int two = method.Body.Instructions[i + 2].GetLdcI4Value();
                            int three = method.Body.Instructions[i + 4].GetLdcI4Value();
                            string opOne = islemDondur(method.Body.Instructions[i + 3].OpCode.ToString());
                            string opTwo = islemDondur(method.Body.Instructions[i + 4].OpCode.ToString());
                            int value2 = (int)(one + two - three);
                            int value = (int)Eval(one, opOne, two, opTwo, three);

                            method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                            method.Body.Instructions[i].Operand = value2;

                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            method.Body.Instructions.RemoveAt(i + 1);
                            /*                 Console.WriteLine($"{one}        {two}       {three}");
                                             Console.WriteLine(value2);*/
                            //          method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;

                            mathcock++;
                        }
                    }
                }
            }
        }

        #region Math
        public static string islemDondur(string deger)
        {
            switch (deger)
            {
                case "div":
                    return "/";
                case "sub":
                    return "-";
                case "add":
                    return "+";
            }
            return "";
        }
        public static double Eval(int bir, string islemBir, int iki, string islemİki, int uc)
        {
            string islem = bir.ToString() + islemBir + iki.ToString() + islemİki + uc.ToString();

            double sonuc = Convert.ToDouble(new DataTable().Compute(islem, null));

            return sonuc;
        }

        public static double EvalSimple(int bir, string islemBir, int iki)
        {
            string islem = bir.ToString() + islemBir + iki.ToString();

            double sonuc = Convert.ToDouble(new DataTable().Compute(islem, null));

            return sonuc;
        }
        #endregion

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
