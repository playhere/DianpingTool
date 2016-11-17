using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace DianpingTool
{
    public static class CombinationTool
    {
        public static IEnumerable<List<List<decimal>>> Assignments(List<List<int>> solutions, List<decimal> prices)
        {
            var result = new List<List<List<decimal>>>();
            var n = prices.Count;
            var listIndexes = new List<int>();
            for (int i = 0; i < n; i++)
            {
                listIndexes.Add(i);
            }
            var ps = new Permutations<int>(listIndexes);
            foreach (var solution in solutions)
            {
                foreach (List<int> p in ps)
                {
                    var assignment = new List<List<decimal>>();
                    var currentIndex = 0;
                    foreach (var setSize in solution)
                    {
                        var set = new List<decimal>();
                        for (int i = 0; i < setSize; i++)
                        {
                            set.Add(prices[p[currentIndex]]);
                            currentIndex++;
                        }
                        assignment.Add(set);

                    }
                    result.Add(assignment);
                }
            }
           // var newResult = result.Distinct(new SolutionComparer());

            return result.AsEnumerable();
        }

        public static List<List<int>> DivideInt(int n, int m, List<int> parentSolution)
        {

            if (n <= 0)
            {
                return null;
            }
            if (n == 1 || m == 1)
            {
                var copySolution = new List<int>(parentSolution);
                if (n == 1)
                {
                    copySolution.Add(1);
                }
                else if (m == 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        copySolution.Add(1);
                    }
                }
                var solutions = new List<List<int>>();
                solutions.Add(copySolution);
                return solutions;
            }
            else if (m > n)
            {
                var copySolution = new List<int>(parentSolution);
                return DivideInt(n, n, copySolution);
            }
            else if (m == n)
            {
                var copySolution1 = new List<int>(parentSolution);
                copySolution1.Add(m);

                var copySolution2 = new List<int>(parentSolution);
                var solutions = DivideInt(n, n - 1, copySolution2);
                solutions.Add(copySolution1);
                return solutions;
            }
            else
            {
                var copySolution1 = new List<int>(parentSolution);
                var solutions1 = DivideInt(n, m - 1, copySolution1);
                var copySolution2 = new List<int>(parentSolution);
                copySolution2.Add(m);
                var solutions2 = DivideInt(n - m, m, copySolution2);
                foreach (var s in solutions2)
                {
                    solutions1.Add(s);
                }
                return solutions1;
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }        
        
    }

    
}
