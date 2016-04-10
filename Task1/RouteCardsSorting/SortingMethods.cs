using System.Collections.Generic;
using System.Linq;

namespace RouteCardsSorting
{
    public static class SortingMethods
    {
        //Идея в том, чтобы поставить произвольный элемент массива в центр почти в двое большего (чем исходный)
        //массива и "приклеивать" все оставшиеся элементы либо, справа либо слева от получающейся цепочки.
        //Это избавит нас от необходимости предваритьного поиска крайнего элемента
        /// <summary>
        /// Сортирует путевые карточки так, чтобы для каждой карточки в упорядоченном списке пункт назначения на ней 
        /// совпадал с пунктом отправления в следующей карточке в списке
        /// </summary>
        /// <param name="cards">Набор путевых карточек</param>
        /// <returns>Отсортированный массив путевых карточек</returns>
        public static RouteCard[] Sort(IEnumerable<RouteCard> cards)
        {
            var arr = cards.ToArray();
            int lengt = arr.Length;
            var res = new RouteCard[lengt * 2 - 1];
            //медиана созданного массива
            var median = lengt - 1;
            //индекс крайнего справа
            var rightIndex = median;
            //индекс крайнего слева
            var leftIndex = median;

            //Запись "концов" упорченного массива даст небольшой выигрыш (~10% для 10000 элементов отсортированных в случайном порядке)
            //крайний левый город 
            var left = arr[0].From;
            //крайний правый город 
            var right = arr[0].To;

            res[median] = arr[0];
            arr[0] = null;

            //Будем проходить по массиву распределяя элементы в итоговый (в начало (индекс leftIndex) или конец(rightIndex)) пока не распределим всех.
            bool notEmpty = true;

            while (notEmpty)
            {
                notEmpty = false;
                
                for (int i = 1; i < lengt; i++)
                {
                    if (arr[i] == null) continue;
                    notEmpty = true;

                    //подходит с правой стороны, т.е. начальный пункт совпадает с конечным из упорядоченной последовательности
                    if (arr[i].From == right)
                    {
                        res[++rightIndex] = arr[i];
                        right = arr[i].To;
                        arr[i] = null;
                    }
                    //подходит с левой стороны
                    else if (arr[i].To == left)
                    {
                        res[--leftIndex] = arr[i];
                        left = arr[i].From;
                        arr[i] = null;
                    }
                }
            }

            var result = new RouteCard[lengt];
            for (int i = 0; i < lengt; i++)
            {
                result[i] = res[leftIndex + i];
            }
            return result;
        }
    }
}
