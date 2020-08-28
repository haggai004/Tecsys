using System.Collections.Generic;

namespace Tecsys.Retail.TypeMapping
{
    public interface ITypeMapper
    {
        TTo Map<TFrom, TTo>(TFrom tfrom);
        List<TTo> Map<TFrom, TTo>(List<TFrom> tfromList);
    }
}