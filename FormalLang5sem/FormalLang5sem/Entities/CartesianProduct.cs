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
        private Dictionary<int, T> _valueByIndex1;  //  in rows
        private Dictionary<T, int> _indexByValue1;  //  in rows
        private Dictionary<int, T> _valueByIndex2;  //  in columns
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
            _valueByIndex1 = set1.ToDictionary(t => k++);
            _indexByValue1 = _valueByIndex1.ToDictionary(t => t.Value, t => t.Key);

            k = 0;
            _valueByIndex2 = set2.ToDictionary(t => k++);
            _indexByValue2 = _valueByIndex2.ToDictionary(t => t.Value, t => t.Key);
        }
    }
}
