using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FormalLang5sem.Entities
{
    class CartesianProduct<T>
    {
        private List<string>[,] _table;
        private Dictionary<T, int> _indexByValue1;  //  in rows
        private Dictionary<T, int> _indexByValue2;  //  in columns


        public int RowsCount;
        public int ColumnsCount;


        public CartesianProduct(ISet<T> set1, ISet<T> set2)
        {
            RowsCount = set1.Count;
            ColumnsCount = set2.Count;
            _table = new List<string>[RowsCount, ColumnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    _table[i, j] = new List<string>();
                }
            }

            IndexesMatching(set1, set2);
        }


        private void IndexesMatching(ISet<T> set1, ISet<T> set2)
        {
            int k = 0;
            _indexByValue1 = set1.ToDictionary(t => t, t => k++);

            k = 0;
            _indexByValue2 = set2.ToDictionary(t => t, t => k++);
        }


        public List<string> this[T itemInRow, T itemInColumn]
        {
            get
            {
                int i = _indexByValue1[itemInRow];
                int j = _indexByValue2[itemInColumn];
                return _table[i, j];
            }
        }
    }
}
