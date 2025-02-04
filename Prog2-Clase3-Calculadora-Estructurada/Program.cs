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

        /// <summary>
        /// Variable para almacenar el resultado de las operaciones de memoria
        /// </summary>
        static double memoriaAlmacenada = 0;    

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
                case 'P': //porcentaje
                    CalcularPorcentaje();
                    break;
                case 'Q': //Elevar al cuadrado
                    CalcularCuadrado();
                    break;
                case 'M': //Submenu de memoria
                    ProcesarOperacionesMemoria();
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

        private static void ProcesarOperacionesMemoria()
        {
            MostrarSubMenuMemoria();
            char tecla = char.ToUpper(Console.ReadKey().KeyChar);
            switch (tecla)
            {
                case '1': //MC
                    memoriaAlmacenada=0;
                    break;
                case '2': //MR
                    memoriaActual = memoriaAlmacenada;
                    nuevoNumero = true;
                    break;
                case '3': //M+
                    memoriaAlmacenada += memoriaActual;
                    nuevoNumero = true;
                    break;
                case '4': //M-
                    memoriaAlmacenada -= memoriaActual;
                    nuevoNumero = true;
                    break;
                case '5': //MS
                    memoriaAlmacenada = memoriaActual;
                    nuevoNumero = true;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Muestra el submenú de operaciones de memoria
        /// </summary>
        private static void MostrarSubMenuMemoria()
        {
            Console.WriteLine("\n=== Operaciones de Memoria ===");
            Console.WriteLine("1 : MC (Limpiar memoria)");
            Console.WriteLine("2 : MR (Recordar memoria)");
            Console.WriteLine("3 : M+ (Sumar a la memoria)");
            Console.WriteLine("4 : M- (Restar a la memoria)");
            Console.WriteLine("5 : MS (Guardar en memoria)");
            Console.WriteLine("X : Volver al menú principal");
            Console.WriteLine("===========================");
        }

        /// <summary>
        /// Elevar al cuadrado el numero actial
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private static void CalcularCuadrado()
        {
            memoriaActual = Math.Pow(memoriaActual, 2);
        }

        /// <summary>
        /// Calcula el porcentaje del numero actual
        /// </summary>
        /// <remarks>
        /// Si hay una operacion pendiente, calcula el porcentaje del primer operando.
        /// Sino, calcula el porcentaje del numero actual.
        /// </remarks>
        static void CalcularPorcentaje()
        {
            if (tieneOperador)
            {
                memoriaActual = (operando * memoriaActual) / 100;
            }
            else
            {
                memoriaActual = memoriaActual / 100;
            }
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
            if (memoriaAlmacenada!=0)
            {
                Console.WriteLine($"Valor en memoria: {memoriaAlmacenada}");
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

            #region Operaciones especiales
            Console.WriteLine("\nOperaciones especiales:");
            Console.WriteLine("P : Porcentaje");
            Console.WriteLine("Q : Elevar al cuadrado (x²)");
            #endregion

            #region Operaciones de memoria
            Console.WriteLine("\nOperaciones de memoria:");
            Console.WriteLine("M : Mostrar submenú de memoria");
            #endregion

            #region Operaciones de uso de comun de la calculadora
            Console.WriteLine("\nOtras operaciones:");
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