using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Core
    {
        int count = 0;
        public Algoritms.Func[,] Funcs;
        public decimal[,] coefs;

        public Core(int newCount) => Count = newCount;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                Funcs = new Algoritms.Func[2, value];
                coefs = new decimal[2, value];
            }
        }

        public bool IsFull()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (Funcs[i, j] == null)
                        return false;
                }
            }
            return true;
        }
        public void CoreChange(int newCount)
        {
            if (newCount == count)
                return;
            ArrChange<Algoritms.Func>(newCount, ref Funcs);
            ArrChange<decimal>(newCount, ref coefs);
            count = newCount;
        }
        void ArrChange<T>(int newCount, ref T[,] arr)
        {
            T[,] f = new T[2, count];
            Array.Copy(arr, f, 2 * count);
            arr = new T[2, newCount];
            int minCount = Math.Min(count, newCount);
            Array.Copy(f, arr, minCount);
            Array.Copy(f, count, arr, newCount, minCount);
        }
        public double[] Solve(Algoritms.Func func, double funcConst, double start, double finish, double lambda)
        {
            double[,] alpha = new double[count, count];
            double[] betta = new double[count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    alpha[i, j] = -1 * lambda * Algoritms.TrapezMetod(start, finish, 
                        Funcs[0, j], (double)coefs[0, j], Funcs[1, i], (double)coefs[1, i]);
                }
                alpha[i, i] += 1;
                betta[i] = Algoritms.TrapezMetod(start, finish,
                    func, funcConst, Funcs[1, i], (double)coefs[1, i]);
            }
            return Algoritms.GaussElimination(alpha, betta);
        }
    }
}
