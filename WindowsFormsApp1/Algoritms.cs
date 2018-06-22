using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Algoritms
    {
        const double INF = 1 / 0.0;
        public delegate double Func(double x, double n);
        public static Func[] ReturnFunc = { Exp, Log, Math.Pow, Sin, Cos, Const };

        static double Exp(double x, double n)
        { return Math.Exp(n * x); }
        static double Cos(double x, double n)
        { return Math.Cos(n * x); }
        static double Sin(double x, double n)
        { return Math.Sin(n * x); }
        static double Const(double x, double n)
        { return n; }
        static double Log(double x, double n)
        { return Math.Log(n * x); }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        static void SwapRows(ref double[,] matrix, ref double[] vector, int i, int j)
        {
            int n = matrix.GetLength(0);
            for (int k = 0; k < n; k++)
            {
                Swap<double>(ref matrix[i, k], ref matrix[j, k]);
            }
            Swap<double>(ref vector[i], ref vector[j]);
        }
        public static double[] GaussElimination(double[,] matrix, double[] vector)
        {
            int n = vector.Length;
            for (int i = 0; i < n; i++)
            {
                if (matrix[i, i] == 0)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (matrix[j, i] != 0)
                        {
                            SwapRows(ref matrix, ref vector, i, j);
                            break;
                        }
                    }
                }
                if (matrix[i, i] != 0)
                {
                    double Aii = matrix[i, i], Vi = vector[i];
                    matrix[i, i] = 1;
                    vector[i] /= Aii;
                    for (int j = i + 1; j < n; j++)
                    {
                        double Aji = matrix[j, i];
                        for (int k = i + 1; k < n; k++)
                        {
                            matrix[j, k] -= Aji * matrix[i, k] / Aii;
                        }
                        vector[j] -= Aji * Vi / Aii;
                    }
                    for (int j = i + 1; j < n; j++)
                    {
                        matrix[i, j] /= Aii;
                    }
                }
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (matrix[i, i] == 0 && vector[i] != 0)
                {
                    vector[0] = INF;
                    break;
                }
                for (int j = i - 1; j >= 0; j--)
                {
                    vector[j] -= vector[i] * matrix[j, i];
                }
            }
            return vector;
        }
        public static double TrapezMetod(double start, double finish, Func A, double n1, Func B, double n2)
        {
            int n = 100;
            double step = (finish - start) / n, res = 0;
            for (int i = 1; i < n; i++)
            {
                res += A(start + step * i, n1) * B(start + step * i, n2);
            }
            res = (res + 0.5 * (A(start, n1) * B(start, n2) + A(finish, n1) * B(finish, n2))) * step;
            return res;
        }

    }
}
