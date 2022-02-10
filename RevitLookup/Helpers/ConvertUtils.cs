using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.Helpers
{
    public static class ConvertUtils
    {
        /// <summary>
        /// Convert IEnumerable To ObservableCollection 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var newSource = new ObservableCollection<T>();
            foreach (var t in source)
            {
                newSource.Add(t);
            }
            return newSource;
        }
    }
}
