using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;

namespace Data.Importing.StageResultContexts
{
    internal class StageResultHelper
    {
        private static readonly ConcurrentDictionary<Type, Func<ImportContext, IStageProcessor, object, StageResultContext>> _delCache = new ConcurrentDictionary<Type, Func<ImportContext, IStageProcessor, object, StageResultContext>>();

        public static Func<ImportContext, IStageProcessor, object, StageResultContext> GetResultDelegate(Type content)
        {
            var del = StageResultHelper._delCache.GetOrAdd(content, k => StageResultHelper.BuildConstructorDelegate(k));
            return del;
        }

        private static Func<ImportContext, IStageProcessor, object, StageResultContext> BuildConstructorDelegate(Type type)
        {
            var typeToConstruct = typeof(ContentStageResult<>).MakeGenericType(type);
            var contextPar = Expression.Parameter(typeof(ImportContext));
            var processorPar = Expression.Parameter(typeof(IStageProcessor));
            var contentPar = Expression.Parameter(typeof(object));
            var contentParConvert = Expression.Convert(contentPar, type);
            var ctor = typeToConstruct.GetConstructor(new[] { typeof(ImportContext), typeof(IStageProcessor), type });
            var newExp = Expression.New(ctor, contentPar, processorPar, contentParConvert);
            var lambda = Expression.Lambda<Func<ImportContext, IStageProcessor, object, StageResultContext>>(newExp, contentPar, processorPar, contentPar);
            var del = lambda.Compile();
            return del;
        }
    }
}