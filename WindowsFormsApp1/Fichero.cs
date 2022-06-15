using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pass
{
    internal static class Fichero
    {
        public static void Escribir(string nombre, string[] lineas)
        {
            try
            {
                File.WriteAllLines(nombre, lineas);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void EscribirMas(string nombre, string[] lineas)
        {
            using (StreamWriter sw = new StreamWriter(nombre, true))
            {
                for (int i = 0; i < lineas.Length; i++)
                {
                    sw.WriteLine(lineas[i]);
                }
            }
        }
    }
}
