using NUnit.Framework;
using System;
using System.Linq;

namespace RouteCardsSorting.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test5ElementsDirectOrder()
        {
            TestDirectOrder(5);
        }

        [Test]
        public void Test5ElementsInvertedOrderSorting()
        {
            TestInvertedOrder(5);
        }

        [Test]
        public void Test5ElementsRandomOrderSorting()
        {
            TestRandomOrder(5);
        }

        [Test]
        public void Test1000ElementsDirectOrder()
        {
            TestDirectOrder(1000);
        }

        [Test]
        public void Test1000ElementsInvertedOrder()
        {
            TestInvertedOrder(1000);
        }

        [Test]
        public void Test1000ElementsRandomOrder()
        {
            TestRandomOrder(1000);
        }

        [Test]
        public void Test10000ElementsDirectOrder()
        {
            TestDirectOrder(10000);
        }

        [Test]
        public void Test10000ElementsInvertedOrder()
        {
            TestInvertedOrder(10000);
        }

        [Test]
        public void Test10000ElementsRandomOrder()
        {
            TestRandomOrder(10000);
        }

        [Test]
        public void Test100000ElementsDirectOrder()
        {
            TestDirectOrder(100000);
        }

        [Test]
        public void Test100000ElementsInvertedOrder()
        {
            TestInvertedOrder(100000);
        }

        //[Test] выполняется порядка 80 секунд
        public void Test100000ElementsRandomOrder()
        {
            TestRandomOrder(100000);
        }


        private static void TestDirectOrder(int count)
        {
            var arr = new RouteCard[count];
            for (int i = 0; i < count; i++)
            {
                arr[i] = new RouteCard { From = i.ToString(), To = (i + 1).ToString() };
            }

            var result = SortingMethods.Sort(arr);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(arr[i], result[i]);
            }
        }

        private static void TestInvertedOrder(int count)
        {
            var arr = new RouteCard[count];
            for (int i = count; i > 0; i--)
            {
                arr[i - 1] = new RouteCard { From = i.ToString(), To = (i - 1).ToString() };
            }

            var result = SortingMethods.Sort(arr);

            for (int i = count; i > 0; i--)
            {
                Assert.AreEqual(arr[count - i], result[i - 1]);
            }
        }

        private static void TestRandomOrder(int count)
        {
            var arr = new RouteCard[count];
            for (int i = 0; i < count; i++)
            {
                arr[i] = new RouteCard { From = i.ToString(), To = (i + 1).ToString() };
            }
            //Для того, чтобы не нарушать условие воспроизводимости теста, зафиксируем зерно рандома
            var rand = new Random(42);
            arr = arr.OrderBy(x => rand.Next()).ToArray();

            var result = SortingMethods.Sort(arr);

            //Требуемый порядок нам не известен, поэтому проверим соответствует ли массив условиям сортировки
            for (int i = 0; i < count - 1; i++)
            {
                Assert.AreEqual(result[i].To, result[i + 1].From);
            }
        }
    }
}
