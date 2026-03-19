using DrMock.EfCore.Exceptions;
using DrMock.EfCore.Models;
using Moq;
using System;
using System.Collections.Generic;

namespace DrMock.EfCore.Helpers
{
    internal static class ConfluenceHelper
    {
        public static void EnsureAtLeastOnePasses<T>(this IEnumerable<Action> actions, EfMethod efMethod)
        {
            var passes = 0;

            foreach (var action in actions)
            {
                try
                {
                    action();

                    passes++;
                }
                catch (MockException _) { }
            }

            if (passes == 0)
                throw DrMockException.CallExpectedNotMade<T>(EfMethod.Add);
        }

        public static void EnsureOnlyOnePasses<T>(this IEnumerable<Action> actions, EfMethod efMethod)
        {
            var passes = 0;

            foreach (var action in actions)
            {
                try
                {
                    action();

                    passes++;
                }
                catch (MockException _) { }
            }

            if (passes == 0)
                throw DrMockException.CallExpectedNotMade<T>(EfMethod.Add);

            if (passes > 1)
                throw DrMockException.CallMadeOnBothContextAndSet<T>(EfMethod.Add);
        }
    }
}