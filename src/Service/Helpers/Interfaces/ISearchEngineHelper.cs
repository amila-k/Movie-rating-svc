using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Service.Helpers.Interfaces
{
    public interface ISearchEngineHelper
    {
        List<Expression<Func<DomainModels.Show, bool>>> GetExpressionBasedOnText(string text);
    }
}
