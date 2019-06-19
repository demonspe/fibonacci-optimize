using System;

namespace FibonacciPower
{
  /// <summary>
  /// Входная точка в программу.
  /// </summary>
  class Program
  {
    #region Поля и свойства

    /// <summary>
    /// Массив для кэширования подсчитанных сленов последовательности Фибоначи.
    /// </summary>
    private static long[] cachedFibonacciMembers = new long[45];

    /// <summary>
    /// Счетчик количества вызовов функции для подсчета n-го члена последовательности.
    /// </summary>
    private static int calls = 0;

    #endregion

    #region Методы

    /// <summary>
    /// Main.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    static void Main(string[] args)
    {
      cachedFibonacciMembers[0] = 0;
      cachedFibonacciMembers[1] = 1;
      long result;
      calls = 0;
      var watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberIterative(33);
      watch.Stop();
      var elapsedMs = watch.ElapsedTicks;
      Console.WriteLine($"Итеративный. Время: {elapsedMs} tics. Вызовы: {calls}.");

      calls = 0;
      watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberRecursive(33);
      watch.Stop();
      elapsedMs = watch.ElapsedTicks;
      Console.WriteLine($"Рекурсивный. Время: {elapsedMs} tics. Вызовы: {calls}.");

      calls = 0;
      watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberRecursiveCached(33);
      watch.Stop();
      elapsedMs = watch.ElapsedTicks;
      Console.WriteLine($"Рекурсивный с кэшированием 1. Время: {elapsedMs} tics. Вызовы функции: {calls}.");

      calls = 0;
      watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberRecursiveCached(33);
      watch.Stop();
      elapsedMs = watch.ElapsedTicks;
      Console.WriteLine($"Рекурсивный с кэшированием 2. Время: {elapsedMs} tics. Вызовы функции: {calls}.");

      calls = 0;
      watch = System.Diagnostics.Stopwatch.StartNew();
      result = GetFibonacciMemberRecursiveCached(33);
      watch.Stop();
      elapsedMs = watch.ElapsedTicks;
      Console.WriteLine($"Рекурсивный с кэшированием 3. Время: {elapsedMs} tics. Вызовы функции: {calls}.");
      Console.Read();
    }

    /// <summary>
    /// Итеративный вариант нахождения n-го члена последовательности.
    /// </summary>
    /// <param name="n">Номер члена последовательности который необходимо найти.</param>
    /// <returns>n-й член последовательности.</returns>
    private static long GetFibonacciMemberIterative(int n)
    {
      calls++;
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
      calls++;
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
      calls++;
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
