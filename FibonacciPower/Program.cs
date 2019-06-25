using System;

namespace FibonacciPower
{
  /// <summary>
  /// Входная точка в программу.
  /// </summary>
  class Program
  {
    #region Константы

    /// <summary>
    /// Номер члена последовательности Фибоначи, который необходимо вычислить.
    /// </summary>
    private const int FibonacciMemberNum = 45;

    #endregion

    #region Поля и свойства

    /// <summary>
    /// Массив для кэширования подсчитанных сленов последовательности Фибоначи.
    /// </summary>
    private static long[] cachedFibonacciMembers = new long[FibonacciMemberNum + 1];

    /// <summary>
    /// Счетчик вызовов рекурсивных фенкций.
    /// </summary>
    private static long recursiveCalls;

    #endregion

    #region Методы

    /// <summary>
    /// Main.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    static void Main(string[] args)
    {
      long result;

      Console.Write("Старт: Итеративный... ");
      var watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberIterative(FibonacciMemberNum);
      watch.Stop();
      Console.WriteLine($"Результат: {result}. Время: {watch.Elapsed}.");

      Console.Write("Старт: Рекурсивный... ");
      recursiveCalls = 0;
      watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberRecursive(FibonacciMemberNum);
      watch.Stop();
      Console.WriteLine($"Результат: {result}. Время: {watch.Elapsed}. Вызовы функций: {recursiveCalls}.");

      Console.Write("Старт: Рекурсивный с кэшированием... ");
      recursiveCalls = 0;
      cachedFibonacciMembers[0] = 0;
      cachedFibonacciMembers[1] = 1;
      watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberRecursiveCached(FibonacciMemberNum);
      watch.Stop();
      Console.WriteLine($"Результат: {result}. Время: {watch.Elapsed}. Вызовы функций: {recursiveCalls}.");
      Console.Read();
    }

    /// <summary>
    /// Итеративный вариант нахождения n-го члена последовательности.
    /// </summary>
    /// <param name="n">Номер члена последовательности который необходимо найти.</param>
    /// <returns>n-й член последовательности.</returns>
    private static long GetFibonacciMemberIterative(int n)
    {
      if (n <= 0) return 0;
      if (n == 1) return 1;
      if (n == 2) return 1;
      int[] lastMembers = { 0, 1, 1 };
      for (int i = 3; i <= n; i++)
      {
        lastMembers[0] = lastMembers[1];
        lastMembers[1] = lastMembers[2];
        lastMembers[2] = lastMembers[1] + lastMembers[0];
      }
      return lastMembers[2];
    }

    /// <summary>
    /// Рекурсивный вариант нахождения n-го члена последовательности.
    /// </summary>
    /// <param name="n">Номер члена последовательности который необходимо найти.</param>
    /// <returns>n-й член последовательности.</returns>
    private static long GetFibonacciMemberRecursive(int n)
    {
      recursiveCalls++;
      if (n <= 0) return 0;
      if (n == 1) return 1;
      return GetFibonacciMemberRecursive(n - 2) + GetFibonacciMemberRecursive(n - 1);
    }

    /// <summary>
    /// Рекурсивный вариант нахождения n-го члена последовательности с кэшированием промежуточных результатов.
    /// </summary>
    /// <param name="n">Номер члена последовательности который необходимо найти.</param>
    /// <returns>n-й член последовательности.</returns>
    private static long GetFibonacciMemberRecursiveCached(int n)
    {
      recursiveCalls++;
      if (n <= 0) return cachedFibonacciMembers[0];
      if (n == 1) return cachedFibonacciMembers[1];
      if (cachedFibonacciMembers[n] != 0)
        return cachedFibonacciMembers[n];
      else
      {
        cachedFibonacciMembers[n] = GetFibonacciMemberRecursiveCached(n - 2) + GetFibonacciMemberRecursiveCached(n - 1);
        return cachedFibonacciMembers[n];
      }
    }

    #endregion
  }
}
