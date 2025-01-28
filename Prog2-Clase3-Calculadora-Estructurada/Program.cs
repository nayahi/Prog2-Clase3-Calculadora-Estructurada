using System;

namespace Prog2_Calculadora
{
    class Program
    {
        static double memoriaActual = 0;
        static double operando = 0;
        static char operador = ' ';
        static bool nuevoNumero = true;
        static bool tieneOperador = false;

        static void Main(string[] args)
        {
            MostrarMenu();

            while (true)
            {
                char tecla = char.ToUpper(Console.ReadKey().KeyChar);
                if (tecla == 'X')
                {
                    break;
                }

                ProcesarTecla(tecla);
            }
        }

        static void ProcesarTecla(char tecla)
        {
            if (char.IsDigit(tecla))
            {
                ProcesarNumero(tecla);
            }
            else
            { 
                ProcesarOperacion(tecla);
            }
        }

        static void ProcesarOperacion(char tecla)
        {
            switch(char.ToUpper(tecla))
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    if (!tieneOperador)
                    {
                        operador = tecla;
                        operando = memoriaActual;
                        nuevoNumero = true;
                        tieneOperador = true;
                    }
                    break;
                case 'C':
                    LimpiarTodo();
                    break;
                case 'S':
                    CambiarSigno();
                    break;
                case 'B':
                    BorrarOperando();
                    break;
                case '=':
                    CalcularResultado();
                    break;
                default:
                    break;
            }
            MostrarPantalla();
        }

        static void CalcularResultado()
        {
            if (tieneOperador)
            {
                switch (operador)
                {
                    case '+':
                        memoriaActual = Sumar(operando, memoriaActual);
                        break;
                    case '-':
                        memoriaActual = Restar(operando, memoriaActual);
                        break;
                    case '*':
                        memoriaActual = Multiplicar(operando, memoriaActual);
                        break;
                    case '/':
                        memoriaActual = Dividir(operando, memoriaActual);
                        break;
                    default:
                        break;
                }
                tieneOperador = false;
                nuevoNumero = true;
            }
        }

        static double Sumar(double operando, double memoriaActual) => operando + memoriaActual;
        static double Restar(double operando, double memoriaActual) => operando - memoriaActual;
        static double Multiplicar(double operando, double memoriaActual) => operando * memoriaActual;
        static double Dividir(double operando, double memoriaActual)
        {
            if (memoriaActual == 0)
            {
                Console.WriteLine("No se puede dividir por cero");
                return 0;
            }
            return operando / memoriaActual;
        }

        private static void BorrarOperando()
        {
            memoriaActual = 0;
            nuevoNumero = true;
        }

        private static void CambiarSigno()
        {
            memoriaActual *= -1;
        }

        static void LimpiarTodo()
        {
            memoriaActual = 0;
            operando = 0;
            operador = ' ';
            nuevoNumero = true;
            tieneOperador = false;
        }

        static void ProcesarNumero(char numero)
        {
            if (nuevoNumero)
            {
                memoriaActual = double.Parse(numero.ToString());
                nuevoNumero = false;
            }
            else
            {
                string numeroActual = memoriaActual.ToString() + numero;
                memoriaActual = double.Parse(numeroActual);
            }
            MostrarPantalla();
        }

        static void MostrarPantalla()
        {
            Console.Clear();
            MostrarMenu();
            Console.WriteLine($"\nMemoria actual: {memoriaActual}");
            if (tieneOperador)
            {
                Console.WriteLine($"Operación por hacer: {operando} {operador}");
            }
        }

        static void MostrarMenu()
        {
            #region Titulo
            Console.WriteLine("=============================================");
            Console.WriteLine("Calculadora Básica");
            Console.WriteLine("Operaciones disponibles:");
            #endregion Titulo
            
            #region Operaciones básicas
            Console.WriteLine("+ Sumar");
            Console.WriteLine("- Restar");
            Console.WriteLine("* Multiplicar");
            Console.WriteLine("/ División");
            #endregion Operaciones básicas

            #region Operaciones de uso de comun de la calculadora
            Console.WriteLine("C Borrar todo");
            Console.WriteLine("S Cambiar signo");
            Console.WriteLine("B Borrar el operando");
            Console.WriteLine("= Calcular resultado");
            Console.WriteLine("X Salir");
            #endregion Operaiones de uso de comun de la calculadora

            #region Pie de pagina
            Console.WriteLine("=============================================");
            #endregion Pie de pagina
        }
    }
}