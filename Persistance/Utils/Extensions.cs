using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Utils
{
    public static class Extensions
    {
            /// <summary>
            /// Determine si la collection est null ou ne possède aucun élément
            /// </summary>
            /// <typeparam name="T">Le type de IEnumerable</typeparam>
            /// <param name="enumerable">L'enumerable qui peut être null ou vide</param>
            /// <returns>
            ///     <c>true</c> si IEnumerable est null ou vide; autrement <c>false</c>.
            /// </returns>
            public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
            {
                if (enumerable == null)
                {
                    return true;
                }
                /*  Si c'est une liste, utilisation de la propriété Count pour plus d'efficacité
                 * La propriété Count est O(1) tandis ce que IEnumerable.Count() est O(N). */
                var collection = enumerable as ICollection<T>;
                if (collection != null)
                {
                    return collection.Count < 1;
                }
                return !enumerable.Any();
            }
    }
}
