using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using WebAPI;

namespace UnitTests.Components.Helpers
{
    public static class ContextMockExtensions
    {
        public static IList<T> GetDatabaseValues<T>(this ApplicationContext context) where T : class
        {
            var list = context.Set<T>().ToList();
            list.ForEach(entry => context.Entry(entry).GetDatabaseValues());

            return list;
        }

        public static T GetDatabaseValue<T>(this ApplicationContext context, Guid? entryId) where T : class
        {
            var entry = context.Set<T>().Find(entryId);
            context.Entry(entry).GetDatabaseValues();

            return entry;
        }
    }
}